USE EPVO_test;

DELIMITER $$

DROP PROCEDURE IF EXISTS `Reload_STUDENT` $$

CREATE PROCEDURE `Reload_STUDENT`()
BEGIN

--------students - Информация об обучающихся (таблица 1)

--TRUNCATE TABLE ESUVO_KazNITU_export.STUDENT_SSO_OUT

/*SELECT * FROM ESUVO_KazNITU_export.STUDENT_SSO
SELECT DISTINCT residence FROM ESUVO.students*/

WITH
/*addr1CTE AS(
  SELECT *
  FROM (
    SELECT
      addr.UserID,
      CONCAT(
        IFNULL(cnt.Title, ''),
        IFNULL(CONCAT(', ', loc.Title), ''),
        IFNULL(CONCAT(', ', addr.LocalityText), ''),
        IFNULL(CONCAT(', ', addr.AddressText), '')
      ) AS FullAddress,
      loc.TypeID,
      cnt.ESUVOCitizenshipCountryID,
      loc.ESUVOCenterKatoCode,
      ROW_NUMBER() OVER (PARTITION BY addr.UserID ORDER BY addr.ID DESC) AS rn
    FROM KAZNITU_export.Edu_UserAddresses addr
    LEFT JOIN KAZNITU_export.Edu_Localities loc ON addr.LocalityID = loc.ID
    LEFT JOIN KAZNITU_export.Edu_Countries cnt ON addr.CountryID = cnt.ID
    WHERE addr.AddressTypeID = 1 -- Место проживания
  ) t
  WHERE rn = 1
)*/
addr1CTE AS (
  SELECT *
  FROM (
    SELECT
      addr.UserID,
      CONCAT(
        IFNULL(cnt.Title, ''),
        IFNULL(CONCAT(', ', reg.Title), ''),
        IFNULL(CONCAT(', ', locArea.Title), ''),
        IFNULL(CONCAT(', ', loc.Title), ''),
        IFNULL(CONCAT(', ', addr.LocalityText), ''),
        IFNULL(CONCAT(', ', addr.AddressText), '')
      ) AS FullAddress,
      cnt.ESUVOCitizenshipCountryID,
      loc.ESUVOCenterKatoCode,
      loc.TypeID,
      reg.Title AS Oblast,
      locArea.Title AS Raion,
      locAreaS.Title AS Area,
      addr.AddressText AS Ulica,
      ROW_NUMBER() OVER (PARTITION BY addr.UserID ORDER BY addr.ID DESC) AS rn
    FROM KAZNITU_export.Edu_UserAddresses addr
    LEFT JOIN KAZNITU_export.Edu_Localities loc ON addr.LocalityID = loc.ID
    LEFT JOIN KAZNITU_export.Obsolete_Edu_Regions reg ON addr.Region = reg.ID
    LEFT JOIN KAZNITU_export.Edu_Localities locArea ON loc.ParentID = locArea.ID
    LEFT JOIN KAZNITU_export.Edu_Countries cnt ON addr.CountryID = cnt.ID
    LEFT JOIN KAZNITU_export.Edu_Localities locAreaS ON addr.Area = locAreaS.ID
    WHERE addr.AddressTypeID = 1 -- и loc.TypeID = 2 -- Место прописки
  ) t
  WHERE rn = 1
),
addr2CTE AS (
  SELECT *
  FROM (
    SELECT
      addr.UserID,
      CONCAT(
        IFNULL(cnt.Title, ''),
        IFNULL(CONCAT(', ', loc.Title), ''),
        IFNULL(CONCAT(', ', addr.LocalityText), ''),
        IFNULL(CONCAT(', ', addr.AddressText), '')
      ) AS FullAddress,
      loc.ESUVOCenterKatoCode,
      ROW_NUMBER() OVER (PARTITION BY addr.UserID ORDER BY addr.ID DESC) AS rn
    FROM KAZNITU_export.Edu_UserAddresses addr
    LEFT JOIN KAZNITU_export.Edu_Localities loc ON addr.LocalityID = loc.ID
    LEFT JOIN KAZNITU_export.Edu_Countries cnt ON addr.CountryID = cnt.ID
    WHERE addr.AddressTypeID = 2 -- Место проживания
  ) t
  WHERE rn = 1
),
attestCTE AS (
  SELECT *
  FROM (
    SELECT
      UserID, Number, IssuedOn, Series, DocumentSubTypeID,
      ROW_NUMBER() OVER (PARTITION BY UserID ORDER BY IssuedOn DESC) AS rn
    FROM KAZNITU_export.Edu_UserEducation
    WHERE DocumentTypeID = 1 -- Аттестат
  ) t
  WHERE rn = 1
),
diplomCTE AS (
  SELECT *
  FROM (
    SELECT
      UserID, Number, IssuedOn, DocumentSubTypeID,
      ROW_NUMBER() OVER (PARTITION BY UserID ORDER BY DocumentTypeID DESC, IssuedOn DESC) AS rn
    FROM KAZNITU_export.Edu_UserEducation
    WHERE DocumentTypeID IN (2, 3) -- 2 - Диплом; 3 - Диплом по магистратуре
  ) t
  WHERE rn = 1
),
grantCTE AS (
  SELECT *
  FROM (
    SELECT
      UserID, Number,
      ROW_NUMBER() OVER (PARTITION BY UserID ORDER BY DocumentTypeID DESC, IssuedOn DESC) AS rn
    FROM KAZNITU_export.Edu_UserDocuments
    WHERE DocumentTypeID = 16 -- Свидетельство о гранте
  ) t
  WHERE rn = 1
),
enterexamCTE AS (
  SELECT *
  FROM (
    SELECT
      UserID,
      CASE DocumentTypeID
        WHEN 15 THEN 'ЕНТ'
        WHEN 17 THEN 'КТА'
      END AS EntrantExamType,
      CASE DocumentTypeID
        WHEN 15 THEN 'UNT'
        WHEN 17 THEN 'CTE'
      END AS EntrantExamTypeEng,
      CASE DocumentTypeID
        WHEN 15 THEN 'БҰТ'
        WHEN 17 THEN 'ТКТ'
      END AS EntrantExamTypeKaz,
      ROW_NUMBER() OVER (PARTITION BY UserID ORDER BY DocumentTypeID DESC, IssuedOn DESC) AS rn
    FROM KAZNITU_export.Edu_UserDocuments
    WHERE DocumentTypeID IN (15, 17) -- Свидетельство о гранте
  ) t
  WHERE rn = 1
),
udlCTE AS (
  SELECT *
  FROM (
    SELECT
      d.UserID, d.Number, d.IssuedOn,
      CASE d.DocumentTypeID
        WHEN 1 THEN 1 -- удостоверение личности
        WHEN 2 THEN 2 -- паспорт
        WHEN 3 THEN 5 -- другой документ
        WHEN 4 THEN 3 -- свидетельство о рождении
      END AS ESUVO_DocumentTypeID,
      IFNULL(d.IssuedByText, o.Title) AS IssuedByText,
      ROW_NUMBER() OVER (PARTITION BY d.UserID ORDER BY d.DocumentTypeID, d.IssuedOn DESC) AS rn
    FROM KAZNITU_export.Edu_UserDocuments d
    LEFT JOIN KAZNITU_export.Edu_DocumentIssueOrgs o ON d.IssuedByID = o.ID
    WHERE d.DocumentTypeID IN (
      1, -- Удостоверение личности
      2, -- Паспорт
      3, -- Удостоверение лица без гражданства
      4  -- Свидетельство о рождении
    )
  ) t
  WHERE rn = 1
),
prevDocCTE AS (
  SELECT *
  FROM (
    SELECT
      d.UserID,
      CONCAT(
        IFNULL(dt.Title, ''),
        IFNULL(CONCAT(' ', d.Number), ''),
        IFNULL(CONCAT(' от ', DATE_FORMAT(d.IssuedOn, '%d.%m.%Y')), '')
      ) AS DocNumber,
      d.DocumentTypeID,
      ROW_NUMBER() OVER (PARTITION BY d.UserID ORDER BY d.DocumentTypeID DESC) AS rn
    FROM KAZNITU_export.Edu_UserEducation d
    JOIN KAZNITU_export.Edu_EducationDocumentTypes dt ON d.DocumentTypeID = dt.ID
  ) t
  WHERE rn = 1
),
katoCTE AS (
  SELECT REPLACE(nameru, ' ', '') AS nameru, MIN(code) AS code
  FROM EPVO_test.CENTER_KATO
  GROUP BY REPLACE(nameru, ' ', '')
  HAVING COUNT(*) = 1
),
studentCreditsSum AS (
  SELECT stc.StudentID, SUM(sc.EctsCredits) AS EctsCredits
  FROM KAZNITU_export.Edu_StudentCourses stc
  JOIN KAZNITU_export.Edu_SemesterCourses sc ON sc.ID = stc.SemesterCourseID
  GROUP BY stc.StudentID
)
-- INSERT INTO STUDENT_SSO (universityId, studentId, firstName, lastName, patronymic, birthDate, startDate, address, nationId, studyFormId, paymentFormId, studyLanguageId, photo,
--             professionid, courseNumber, transcriptNumber, transcriptSeries, isMarried, icNumber, icDate, education, hasExcellent, startOrder, isStudent, certificate,
--             grantNumber, gpa, currentCreditsSum, residence, sitizenshipId, dormState, isInRetire, fromId, local, city, contractId, specializationId, iinPlt, altynBelgi,
--             dataVydachiAttestata, dataVydachiDiploma, dateDocEducation, endCollege, endHighSchool, endSchool, icSeries, icType, livingAddress, nomerAttestata, otherBirthPlace,
--             seriesNumberDocEducation, seriyaAttestata, seriyaDiploma, schoolName, facultyId, sexId, mail, phone, sumPoints, sumPointsCreative, enrollOrderDate,
--             mobilePhone, grantType, academicMobility, incorrectIin, birthPlaceCatoId, livingPlaceCatoId, registrationPlaceCatoId, naselennyiPunktAttestataCatoId, fundingId,
--             typeCode)
SELECT
  29 AS universityId, -- Строка 2147483647 Дополнительная информация
  IFNULL(u.ESUVOID, 60000000 + stud.StudentID) AS studentId, -- Целые числа Уникальный идентификатор записи 10060698
  u.FirstName AS firstname, -- Строка 128 Имя
  u.LastName AS lastname, -- Строка 128 Фамилия
  u.MiddleName AS patronymic, -- Строка 128 Отчество
  u.DOB AS birthDate, -- Дата в формате "yyyy-MM-dd" Дата рождения
  IFNULL(stud.EntryDate, IF(stud.Year > 1, '2022-08-22', '2023-08-22')) AS startDate, -- Дата в формате "yyyy-MM-dd" Дата поступления
  IFNULL(addr1.FullAddress, 'N/A') AS `address`, -- Строка 512 Адрес прописки
  nat.Center_NationalitiesId AS nationid, -- Целые числа Национальность ../nationalities/all id
  CASE
    WHEN spec.LevelID = 1 AND stud.EducationTypeID = 1 AND (r.SemesterCount = 8 OR r.SemesterCount = 7) THEN 1 -- дневное 4г
    WHEN spec.LevelID = 3 THEN 2 -- дневное(PhD)
    WHEN spec.LevelID = 2 AND r.SemesterCount >= 3 THEN 3 -- дневное(master) 2г
    WHEN spec.LevelID = 1 AND stud.EducationTypeID = 2 AND (r.SemesterCount = 5 OR r.SemesterCount = 6) THEN 4 -- заочное(ИДО_3г)
    WHEN spec.LevelID = 2 AND r.SemesterCount < 3 THEN 5
    WHEN spec.LevelID = 1 AND stud.EducationTypeID = 1 AND (r.SemesterCount = 5 OR r.SemesterCount = 6) THEN 6 -- дневное 3г
    WHEN spec.LevelID = 1 AND stud.EducationTypeID = 1 AND (r.SemesterCount = 10 OR r.SemesterCount = 9) THEN 7 -- Дневное 5г
    WHEN spec.LevelID = 1 AND stud.EducationTypeID = 2 AND (r.SemesterCount = 4 OR r.SemesterCount = 3) THEN 8 -- заочное(ИДО_2г)
    -- заочное(ИДО_3к)
    WHEN spec.LevelID = 1 AND stud.EducationTypeID = 2 AND r.SemesterCount = 7 THEN 10 -- заочное(ИДО_3.5г)
    ELSE 1
  END AS studyformid, -- Целые числа Форма обучения ../studyforms/all id
  IF(stud.EducationPaymentTypeID = 1, 2, 1) AS paymentformid, -- Целые числа Форма оплаты ../paymentforms/all id у нас в базе нету по обмену
  IFNULL((SELECT l.Center_StudyLang_Id FROM EPVO_test.STUDYLANGUAGES l WHERE l.Id = stud.StudyLanguageID), 1) AS studylanguageid, -- Целые числа Язык обучения ../studylanguages/all id
  NULL AS photo, -- Бинарные данные в кодировке Base64 Фотография
  IF(spec.Classifier = 1, specz.ESUVOID, spec.ESUVOID) AS professionid, -- Целые числа Код специальности ../professions/get professionID
  -- YEAR(NOW()) - r.Year + IF(MONTH(NOW()) < 9, 0, 1)
  stud.Year AS coursenumber, -- Целые числа Курс. Значение 0 выставляется для выпускников вуза.
  NULL AS transcriptNumber, -- Номер транскрипта. Строка
  NULL AS transcriptSeries, -- Серия транскрипта. Строка
  IF(u.MaritalStatusID = 1, 1, 2) AS ismarried, -- Целые числа Семейное положение ../maritalstates/all id
  IF(u.IIN REGEXP '^[0-9]{12}$', IFNULL(udl.Number, 'N/A'), u.IIN) AS icnumber, -- Строка 256 Номер документа, удостоверяющего личность
  IFNULL(udl.IssuedOn, '2023-01-01') AS icDate, -- Дата документа, удостоверяющего личность: Строка, не указывать null. Дата в формате "yyyy-MM-dd"
  'NA' AS education, -- Строка 4096 Наименование образовательного учреждения
  IFNULL(IF(attest.DocumentSubTypeID = 1 OR attest.DocumentSubTypeID = 2 OR attest.DocumentSubTypeID = 4, 1, 0), 0) AS hasexcellent, -- Логический (true/false) Обладатель аттестата о среднем образовании с отличием
  'NA' AS startorder, -- Строка 256 Номер приказа о зачислении на 1 курс
  CASE WHEN stud.StatusID = 2 THEN 3 ELSE 1 END AS isstudent, -- Целые числа Статус обучающегося (1 – Обучающийся, 2 – Абитуриент, 3 – Выпускник, Отчислен)
  'NA' AS certificate, -- Строка 128 Номер сертификата ЕНТ/КТ
  grants.Number AS grantnumber, -- Строка 128 Номер свидетельства о гранте
  IFNULL(stud.GPA, 0) AS gpa, -- Вещественные числа GPA за весь период обучения
  IFNULL(CredSum.EctsCredits, 0) AS currentCreditsSum, -- Сумма кредитов: Вещественные числа, не указывать null
  IF(addr1.TypeID = 3, 1, IFNULL(addr1.TypeID, 2)) AS residence, -- Целые числа Место окончания школы (город, село) ../residence_states/all id
  -- IFNULL(cnt.ESUVOCitizenshipCountryID, 398) AS sitizenshipid,
  IFNULL(Epvo_Countr.Id, 113) AS sitizenshipid, -- Целые числа Гражданство ../sitizenship_states/all id
  IF(stud.NeedsDorm = 1, 3, 1) AS dormState, -- Целые числа Метка о необходимости предоставления места в общежитии ../dorm_states/all id
  IF(stud.StatusID IN (4, 5), 1, 0) AS isinretire, -- Логический (true/false) Поле показывает, находится ли обучающийся в академическом отпуске
  countr.ESUVOCitizenshipCountryID AS fromid, -- Целые числа Страна прибытия ../sitizenship_states/all id
  IF(addr1.FullAddress LIKE '%алматы%', 1, 0) AS `local`, -- Логический (true/false) Местный(ая) (по отношению к городу, где находится вуз)
  addr1.FullAddress AS city, -- Строка 256 Населенный пункт, откуда прибыл обучающийся
  NULL AS contractid, -- Целые числа Источник оплаты ../ucontracts/all contractid
  IF(spec.Classifier = 1, IFNULL(spec.ESUVOID, spc.Id), spc.Id) AS specializationid, -- Целые числа Код специализации ../specializations/get id
  IF(u.IIN REGEXP '^[0-9]{12}$', u.IIN, NULL) AS iinplt, -- Строка 12 ИИН
  stud.AltynBelgi AS altynBelgi, -- Логический (true/false) Обладатель нагрудного знака Алтын белги
  attest.IssuedOn AS datavydachiattestata, -- Дата в формате "yyyy-MM-dd" Дата выдачи аттестата
  diplom.IssuedOn AS datavydachidiploma, -- Дата в формате "yyyy-MM-dd" Дата выдачи диплома
  NULL AS dateDocEducation, -- Дата образования: Дата в формате "yyyy-MM-dd"
  0 AS endCollege, -- Логический (true/false) Закончил образовательное учреждение по типу колледж
  IF(prevDoc.DocumentTypeID IN (2, 3), 1, 0) AS endHighSchool, -- Логический (true/false) Закончил образовательное учреждение по типу ВУЗ
  IF(prevDoc.DocumentTypeID = 1, 1, 0) AS endSchool, -- Логический (true/false) Закончил образовательное учреждение по типу школа
  NULL AS icseries, -- Строка 255 Серия документа, удостоверяющего личность
  IF(u.IIN REGEXP '^[0-9]{12}$', IFNULL(udl.ESUVO_DocumentTypeID, 1), 2) AS ictype, -- Целые числа Вид документа, удостоверяющего личность ../ictype/all id
  addr2.FullAddress AS livingAddress, -- Строка 512 Адрес проживания
  attest.Number AS nomerattestata, -- Строка 128 Номер аттестата о среднем образовании
  u.PlaceOfBirth AS otherBirthPlace, -- Строка 512 Населенный пункт рождения
  NULL AS seriesNumberDocEducation, -- Серия доп образования: Строка
  attest.Series AS seriyaattestata, -- Строка 128 Серия аттестата
  diplom.Number AS seriyaDiploma, -- Серия диплома: Строка
  NULL AS schoolName, -- Название школы: Строка
  IFNULL(fuc.FacultyId, ouFuc.ID) AS facultyId, -- Идентификатор факультета: Целое число, не указывать null
  CASE u.Male WHEN 1 THEN 2 WHEN 0 THEN 1 END AS sexid, -- Целые числа Пол ../sexes/all id (1 - female; 2 - male)
  IFNULL(u.PersonalEmail, u.Email) AS mail, -- Строка 255 E-mail
  u.HomePhone AS phone, -- Строка 255 Телефон
  NULL AS sumPoints, -- Целые числа Количество набранных баллов для поступления на нетворческие специальности
  NULL AS sumPointsCreative, -- Целые числа Количество набранных баллов для поступления на творческие специальности
  -- IFNULL(IF(stud.Year = 1, '2024-08-02', entr.RegisteredOn), '2023-08-22') AS enrollOrderDate,
  IFNULL(stud.EntryDate, entr.RegisteredOn) AS enrollOrderDate, -- Дата в формате "yyyy-MM-dd" Дата приказа о зачислении на 1 курс
  u.MobilePhone AS mobilePhone, -- Строка 16 Мобильный телефон в формате +7(XXX)XXX-XX-XX
  IFNULL(IF(ept.ESUVOGrantTypeId = 1, gtype.ESUVOGrantTypeId, ept.ESUVOGrantTypeId), 0) AS grant_type, -- Целые числа Вид гранта ../grant_types/all id
  NULL AS academicMobility, -- Академическая мобильность: Целое число
  IF(u.IIN REGEXP '^[0-9]{12}$', 1, 0) AS incorrectIin, -- Логический (true/false) Должно принимать значение true, если заданный ИИН обучающегося верен и выдан государственным органом
  kato.ESUVOCenterKatoCode AS birthPlaceCatoId, -- Строка 512 Населенный пункт рождения (като) ../center_kato/all code
  -- addr2.ESUVOCenterKatoCode AS livingPlaceCatoId,
  ck.Id AS livingPlaceCatoId, -- Строка 512 Населенный пункт проживания (като) ../center_kato/all code
  -- addr1.ESUVOCenterKatoCode AS registrationPlaceCatoId,
  ckReg.Id AS registrationPlaceCatoId, -- Строка 512 Населенный пункт прописки (като) ../center_kato/all code
  NULL AS naselennyiPunktAttestataCatoId, -- Населенный пункт аттестата: Целое число
  stud.FundingID AS fundingId,
  'STUDENT' AS typeCode
FROM KAZNITU_export.Edu_Students stud
LEFT JOIN KAZNITU_export.Edu_Users u ON stud.StudentID = u.ID
JOIN KAZNITU_export.Edu_Rups r ON stud.RupID = r.ID
LEFT JOIN studentCreditsSum CredSum ON CredSum.StudentID = stud.StudentID
LEFT JOIN KAZNITU_export.Edu_EducationPaymentTypes ept ON stud.EducationPaymentTypeID = ept.ID
LEFT JOIN KAZNITU_export.Edu_Specialities spec ON spec.ID = stud.SpecialityID
LEFT JOIN KAZNITU_export.Edu_OrgUnits ou ON ou.ID = spec.RupEditorOrgUnitID
LEFT JOIN KAZNITU_export.Edu_OrgUnits ouFuc ON ouFuc.ID = ou.ParentID AND ouFuc.TypeID = 2
LEFT JOIN EPVO_test.FACULTIES fuc ON fuc.FacultyId = ouFuc.ID
LEFT JOIN LATERAL (
  SELECT sss.SpecializationId, sss.SpecialityId
  FROM KAZNITU_export.Edu_SpecialitySpecializations sss
  WHERE sss.SpecializationId = spec.ID AND sss.SpecialityId < 398
  LIMIT 1
) spz ON TRUE
-- LEFT JOIN KAZNITU_export.Edu_SpecialitySpecializations spz ON spz.SpecializationId = spec.ID AND spz.SpecialityId < 398
-- LEFT JOIN KAZNITU_export.Edu_Specializations specz ON spz.SpecialityId = specz.Id
LEFT JOIN LATERAL (
  SELECT sss.ESUVOID
  FROM KAZNITU_export.Edu_Specializations sss
  WHERE sss.Id = spz.SpecialityId
  ORDER BY sss.ESUVOID
  LIMIT 1
) specz ON TRUE
LEFT JOIN addr1CTE addr1 ON stud.StudentID = addr1.UserID
LEFT JOIN addr2CTE addr2 ON stud.StudentID = addr2.UserID
LEFT JOIN attestCTE attest ON stud.StudentID = attest.UserID
LEFT JOIN diplomCTE diplom ON stud.StudentID = diplom.UserID
LEFT JOIN grantCTE grants ON stud.StudentID = grants.UserID
LEFT JOIN enterexamCTE enterexam ON stud.StudentID = enterexam.UserID
LEFT JOIN KAZNITU_export.Edu_GrantTypes gtype ON stud.GrantTypeID = gtype.ID
LEFT JOIN udlCTE udl ON stud.StudentID = udl.UserID
-- LEFT JOIN katoCTE kato ON REPLACE(u.PlaceOfBirth, ' ', '') = kato.nameru
LEFT JOIN LATERAL (
  SELECT *
  FROM KAZNITU_export.Edu_Localities
  WHERE Title = u.PlaceOfBirth
  LIMIT 1
) kato ON TRUE
LEFT JOIN prevDocCTE prevDoc ON prevDoc.UserID = stud.StudentID
LEFT JOIN KAZNITU_export.StudentInfo_Translations mname_en
  ON mname_en.TableName = 'Edu_Users' AND mname_en.Language = 'en' AND mname_en.ColumnName = 'MiddleName' AND mname_en.ObjectID = stud.StudentID
LEFT JOIN KAZNITU_export.StudentInfo_Translations lname_en
  ON lname_en.TableName = 'Edu_Users' AND lname_en.Language = 'en' AND lname_en.ColumnName = 'LastName' AND lname_en.ObjectID = stud.StudentID
LEFT JOIN KAZNITU_export.StudentInfo_Translations fname_en
  ON fname_en.TableName = 'Edu_Users' AND fname_en.Language = 'en' AND fname_en.ColumnName = 'FirstName' AND fname_en.ObjectID = stud.StudentID
LEFT JOIN KAZNITU_export.Edu_Entrants entr ON entr.EntrantID = stud.StudentID AND entr.StatusID IN (3, 4) -- 3 - Зачислен; 4 - Студент
LEFT JOIN KAZNITU_export.Edu_Countries countr ON countr.ID = u.CitizenshipCountryID
LEFT JOIN KAZNITU_export.Edu_Countries cnt ON u.CitizenshipCountryID = cnt.ID
LEFT JOIN EPVO_test.center_countries Epvo_Countr ON Epvo_Countr.Country_Code = cnt.ESUVOCitizenshipCountryID
LEFT JOIN EPVO_test.NATIONALITIES nat ON nat.Id = u.NationalityID
-- LEFT JOIN EPVO_test.SPECIALIZATIONS spc ON spc.specializationCode = spec.Code
LEFT JOIN LATERAL (
  SELECT *
  FROM EPVO_test.SPECIALITIES_EPVO_2025
  WHERE SpecializationCode = spec.Code
  LIMIT 1
) spc ON TRUE
LEFT JOIN EPVO_test.CENTER_KATO ck ON ck.Code = addr2.ESUVOCenterKatoCode
LEFT JOIN EPVO_test.CENTER_KATO ckReg ON ckReg.Code = addr1.ESUVOCenterKatoCode
WHERE stud.StatusID != 2
  AND stud.CategoryID = 1
  AND stud.StatusID IS NOT NULL -- убрали дубликаты со старого портала
  AND u.DOB IS NOT NULL
  AND NOT EXISTS (
    SELECT 1
    FROM EPVO_test.STUDENT_SSO
    WHERE StudentId = IFNULL(u.ESUVOID, 60000000 + stud.StudentID) AND IsStudent = 1
  )
  AND NOT EXISTS (
    SELECT 1
    FROM EPVO_test.STUDENT
    WHERE StudentId = IFNULL(u.ESUVOID, 60000000 + stud.StudentID) AND IsStudent = 1
  );

END $$

DELIMITER ;
