-- Auto-generated from Domain.Entities.Real and EF Core DbContext mappings.
-- Source DbContexts: SsoDbContext, EpvoSsoDbContext
IF DB_ID(N'KAZNITU_export') IS NULL CREATE DATABASE [KAZNITU_export];
GO

IF DB_ID(N'EPVO_test') IS NULL CREATE DATABASE [EPVO_test];
GO

USE [KAZNITU_export];
GO

IF OBJECT_ID(N'dbo.Edu_Users', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_Users] (
  [ID] int NOT NULL,
  [LastName] nvarchar(max) NOT NULL,
  [FirstName] nvarchar(max) NULL,
  [MiddleName] nvarchar(max) NULL,
  [Email] nvarchar(max) NULL,
  [PersonalEmail] nvarchar(max) NULL,
  [DOB] date NULL,
  [PlaceOfBirth] nvarchar(max) NULL,
  [Male] bit NULL,
  [HomePhone] nvarchar(max) NULL,
  [MobilePhone] nvarchar(max) NULL,
  [IIN] nvarchar(max) NULL,
  [PhotoFileName] nvarchar(255) NULL,
  [PhotoFileData] varbinary(max) NULL,
  [LastUpdatedBy] nvarchar(max) NOT NULL,
  [LastUpdatedOn] datetime2(6) NOT NULL,
  [FileContainerID] uniqueidentifier NULL,
  [MobilePushID] nvarchar(max) NULL,
  [oldId] int NULL,
  [FullName] AS (LTRIM(RTRIM(CONCAT(ISNULL([LastName], N''), N' ', ISNULL([FirstName], N''), N' ', ISNULL([MiddleName], N''))))),
  [ShortName] AS (LTRIM(RTRIM(CONCAT(ISNULL([LastName], N''), N' ', ISNULL(LEFT([FirstName], 1), N''), N'. ', ISNULL(LEFT([MiddleName], 1), N''), N'.')))),
  [ESUVOID] int NULL,
  [ExtraFileContainerID] uniqueidentifier NULL,
  [Resident] bit NOT NULL,
  [Hero_Person_ID] int NULL,
  [IsReadTeamsNotif] bit NULL,
  [NationalityID] int NULL,
  [MaritalStatusID] int NULL,
  [MessengerTypeID] int NULL,
  [CitizenshipCountryID] int NULL,
  [CitizenCategoryID] int NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_Students', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_Students] (
  [StudentID] int NOT NULL,
  [SpecialityID] int NULL,
  [StatusID] int NULL,
  [CategoryID] int NULL,
  [NeedsDorm] bit NOT NULL,
  [AltynBelgi] bit NOT NULL,
  [Year] int NOT NULL,
  [RupID] int NULL,
  [EntryDate] date NULL,
  [GPA] real NULL,
  [LastUpdatedBy] nvarchar(max) NOT NULL,
  [LastUpdatedOn] datetime2(6) NOT NULL,
  [GraduatedOn] datetime2(6) NULL,
  [AcademicStatusEndsOn] date NULL,
  [AcademicStatusStartsOn] date NULL,
  [GPA_Y] real NULL,
  [IsPersonalDataComplete] bit NULL,
  [HosterPrivelegeID] int NULL,
  [MinorSpecialityID] int NULL,
  [EnrollmentTypeId] int NULL,
  [EctsGPA] real NULL,
  [EctsGPA_Y] real NULL,
  [IsScholarship] bit NULL,
  [ScholarshipTypeID] int NULL,
  [ScholarshipOrderNumber] nvarchar(max) NULL,
  [ScholarshipOrderDate] date NULL,
  [ScholarshipDateStart] date NULL,
  [ScholarshipDateEnd] date NULL,
  [FundingID] int NULL,
  [IsKNB] bit NULL,
  [EducationTypeID] int NULL,
  [EducationPaymentTypeID] int NULL,
  [GrantTypeID] int NULL,
  [EducationDurationID] int NULL,
  [StudyLanguageID] int NULL,
  [AcademicStatusID] int NULL,
  [AdvisorID] int NULL,
  PRIMARY KEY ([StudentID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_Employees', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_Employees] (
  [ID] int NOT NULL,
  [IsAdvisor] bit NOT NULL,
  [RoleGroupId] int NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_EmployeePositions', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_EmployeePositions] (
  [ID] int NOT NULL,
  [StartedOn] date NOT NULL,
  [EndedOn] date NULL,
  [LastUpdatedBy] nvarchar(max) NOT NULL,
  [LastUpdatedOn] datetime2(6) NOT NULL,
  [Rate] real NULL,
  [IsMainPosition] bit NULL,
  [HrOrderId] int NULL,
  [OrgUnitID] int NOT NULL,
  [PositionID] int NOT NULL,
  [EmployeeID] int NOT NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_AcademicStatuses', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_AcademicStatuses] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NOT NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_CitizenCategories', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_CitizenCategories] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_Countries', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_Countries] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NULL,
  [ESUVOCitizenshipCountryID] int NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_EducationDurations', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_EducationDurations] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NOT NULL,
  [ShortTitle] nvarchar(max) NULL,
  [NoBDIId] nchar(100) NULL,
  [LevelID] int NOT NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_EducationPaymentTypes', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_EducationPaymentTypes] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NOT NULL,
  [ESUVOGrantTypeId] int NULL,
  [NoBDID] nchar(50) NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_EducationTypes', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_EducationTypes] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NULL,
  [NoBDID] nvarchar(max) NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_GrantTypes', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_GrantTypes] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NULL,
  [ESUVOGrantTypeId] int NULL,
  [Deleted] bit NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_Languages', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_Languages] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NULL,
  [NoBDID] nvarchar(max) NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_MaritalStatuses', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_MaritalStatuses] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_Messengers', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_Messengers] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NOT NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_Nationalities', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_Nationalities] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_PositionCategories', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_PositionCategories] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NOT NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_Positions', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_Positions] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NULL,
  [Deleted] bit NOT NULL,
  [Description] nvarchar(max) NULL,
  [Lectures] int NOT NULL,
  [Practices] int NOT NULL,
  [Labs] int NOT NULL,
  [CategoryID] int NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_SpecialityLevels', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_SpecialityLevels] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NOT NULL,
  [NoBDID] nvarchar(max) NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_OrgUnitTypes', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_OrgUnitTypes] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_OrgUnits', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_OrgUnits] (
  [ID] int NOT NULL,
  [ParentID] int NULL,
  [Title] nvarchar(max) NOT NULL,
  [Deleted] bit NOT NULL,
  [ShortTitle] nvarchar(max) NULL,
  [TypeID] int NOT NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_DocumentIssueOrgs', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_DocumentIssueOrgs] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_SchoolSubjects', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_SchoolSubjects] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NOT NULL,
  [Number] nvarchar(max) NOT NULL,
  [IsRequired] bit NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_Specialities', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_Specialities] (
  [ID] int NOT NULL,
  [Code] nvarchar(max) NOT NULL,
  [Title] nvarchar(max) NOT NULL,
  [YearsOfStudy] int NULL,
  [Diaspora] bit NULL,
  [VillageQuota] bit NULL,
  [Deleted] bit NOT NULL,
  [ShortTitle] nvarchar(max) NULL,
  [Description] nvarchar(max) NULL,
  [ESUVOID] int NULL,
  [Classifier] bit NULL,
  [EducationalProgramStatus] int NULL,
  [EducationalProgramType] int NULL,
  [Classifier2] int NULL,
  [NoBDID] nvarchar(max) NULL,
  [ReadyToSendESUVO] bit NOT NULL,
  [DoubleDiploma] bit NULL,
  [Jointep] bit NULL,
  [Is_interdisciplinary] bit NULL,
  [is_not_active] bit NULL,
  [EducationDuration] decimal(4,1) NULL,
  [isPrikladnoy] bit NULL,
  [LevelID] int NOT NULL,
  [PrimarySubjectID] int NULL,
  [FithSubjectID] int NULL,
  [RupEditorOrgUnitID] int NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_UserDocumentTypes', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_UserDocumentTypes] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NOT NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_UserDocuments', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_UserDocuments] (
  [ID] int NOT NULL,
  [UserID] int NOT NULL,
  [DocumentTypeID] int NOT NULL,
  [IssuedByID] int NULL,
  [IssuedByText] nvarchar(max) NULL,
  [IssuedOn] date NULL,
  [Number] nvarchar(max) NULL,
  [Description] nvarchar(max) NULL,
  [FileName] nvarchar(max) NULL,
  [FileData] varbinary(max) NULL,
  [DescriptionText] nvarchar(max) NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_Rups', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_Rups] (
  [ID] int NOT NULL,
  [SpecialityID] int NOT NULL,
  [SpecialisationID] int NULL,
  [Year] int NOT NULL,
  [SemesterCount] int NOT NULL,
  [AlgorithmID] int NULL,
  [CreditsLimitId] int NULL,
  [IsModular] bit NOT NULL,
  [ApprovedByChair] bit NOT NULL,
  [ApprovedByChairUserID] nvarchar(max) NULL,
  [ApprovedByChairOn] datetime2(6) NULL,
  [ApprovedByOR] bit NOT NULL,
  [ApprovedByORUserID] nvarchar(max) NULL,
  [ApprovedByOROn] datetime2(6) NULL,
  [Locked] bit NOT NULL,
  [EducationDurationID] int NULL,
  [RejectionReason] nvarchar(max) NULL,
  [LastUpdatedBy] nvarchar(max) NOT NULL,
  [LastUpdatedOn] datetime2(6) NOT NULL,
  [AcademicDegreeId] int NULL,
  [RupTitle] nvarchar(max) NULL,
  [IncludeToRegOp] bit NULL,
  [EducationalProgram] bit NULL,
  [EducationalProgramId] int NULL,
  [DualProgram] bit NULL,
  [Hero_WEP_ID] int NULL,
  [RupPrivateSignerRU] nvarchar(max) NULL,
  [RupPrivateSignerEN] nvarchar(max) NULL,
  [RupPrivateSignerKZ] nvarchar(max) NULL,
  [AcademCouncilDate] datetime2(6) NULL,
  [AcademCouncilNum] nvarchar(max) NULL,
  [EducCouncilDate] datetime2(6) NULL,
  [EducCouncilNum] nvarchar(max) NULL,
  [AcademCouncilInstDate] datetime2(6) NULL,
  [AcademCouncilInstNum] nvarchar(max) NULL,
  [EducationDirectionId] int NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_RupAlgorithms', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_RupAlgorithms] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_StudentStatuses', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_StudentStatuses] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NULL,
  [NOBDID] nvarchar(max) NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_StudentCategories', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_StudentCategories] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NOT NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_AddressTypes', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_AddressTypes] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NOT NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_Localities', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_Localities] (
  [ID] int NOT NULL,
  [TypeID] int NOT NULL,
  [Title] nvarchar(max) NOT NULL,
  [ParentID] int NULL,
  [ESUVOCenterKatoCode] nvarchar(max) NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_LocalityTypes', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_LocalityTypes] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NOT NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Obsolete_Edu_Regions', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Obsolete_Edu_Regions] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NOT NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_UserAddresses', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_UserAddresses] (
  [ID] int NOT NULL,
  [UserID] int NOT NULL,
  [AddressTypeID] int NOT NULL,
  [CountryID] int NOT NULL,
  [LocalityID] int NOT NULL,
  [LocalityText] nvarchar(max) NULL,
  [AddressText] nvarchar(max) NULL,
  [Region] nvarchar(max) NULL,
  [Area] nvarchar(max) NULL,
  [AddressTextEN] nvarchar(max) NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_UserEducation', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_UserEducation] (
  [ID] int NOT NULL,
  [UserID] int NOT NULL,
  [SchoolID] int NULL,
  [SchoolText] nvarchar(max) NULL,
  [GraduatedOn] datetime2(6) NULL,
  [DocumentTypeID] int NOT NULL,
  [DocumentSubTypeID] int NULL,
  [Number] nvarchar(max) NULL,
  [Series] nvarchar(max) NULL,
  [IssuedOn] datetime2(6) NULL,
  [GPA] real NULL,
  [StudyLanguageID] int NULL,
  [ExtraInfo] nvarchar(max) NULL,
  [FileContainerID] uniqueidentifier NULL,
  [SpecialityID] int NULL,
  [SpecialityText] nvarchar(max) NULL,
  [Qualification] nvarchar(max) NULL,
  [IsSecondEducation] bit NULL,
  [IsRuralQuota] bit NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_EducationDocumentTypes', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_EducationDocumentTypes] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_EducationDocumentSubTypes', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_EducationDocumentSubTypes] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_Schools', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_Schools] (
  [ID] int NOT NULL,
  [SchoolTypeID] int NOT NULL,
  [SchoolRegionStatusID] int NOT NULL,
  [LocalityID] int NULL,
  [Number] nvarchar(max) NULL,
  [Title] nvarchar(max) NOT NULL,
  [ShortTitle] nvarchar(max) NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_SchoolTypes', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_SchoolTypes] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NOT NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_SchoolRegionStatuses', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_SchoolRegionStatuses] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NOT NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_StudentCourses', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_StudentCourses] (
  [ID] int NOT NULL,
  [StudentID] int NOT NULL,
  [SemesterCourseID] int NOT NULL,
  [RegisteredBy] nvarchar(max) NOT NULL,
  [RegisteredOn] datetime2(6) NOT NULL,
  [Grade1] real NULL,
  [Grade2] real NULL,
  [ExamGrade] real NULL,
  [LetterGrade] nvarchar(max) NULL,
  [LastUpdatedBy] nvarchar(max) NOT NULL,
  [LastUpdatedOn] datetime2(6) NOT NULL,
  [ExtraGrade] nvarchar(max) NULL,
  [LevelID] int NULL,
  [CourseAttributeID] int NULL,
  [prevID] int NULL,
  [MissingPercentage] int NULL,
  [MissingFailure] bit NOT NULL,
  [UnsubmittedGrade1] real NULL,
  [UnsubmittedGrade2] real NULL,
  [Transfer] bit NOT NULL,
  [Ido] bit NULL,
  [IdoSemester] int NULL,
  [ExtraExamGrade] real NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_SemesterCourses', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_SemesterCourses] (
  [ID] int NOT NULL,
  [SemesterID] int NOT NULL,
  [Code] nvarchar(max) NOT NULL,
  [Title] nvarchar(max) NOT NULL,
  [Description] nvarchar(max) NOT NULL,
  [OrgUnitID] int NOT NULL,
  [Credits] decimal(4,1) NOT NULL,
  [EctsCredits] int NOT NULL,
  [ControlTypeID] int NOT NULL,
  [CourseTypeID] int NOT NULL,
  [CourseDVOTypeID] int NULL,
  [Lectures] decimal(4,1) NOT NULL,
  [Practices] decimal(4,1) NOT NULL,
  [Labs] decimal(4,1) NOT NULL,
  [LecturesMinStudentCount] int NOT NULL,
  [PracticesMinStudentCount] int NOT NULL,
  [LabsMinStudentCount] int NOT NULL,
  [LecturesMaxStudentCount] int NOT NULL,
  [PracticesMaxStudentCount] int NOT NULL,
  [LabsMaxStudentCount] int NOT NULL,
  [LastUpdatedBy] nvarchar(max) NOT NULL,
  [LastUpdatedOn] datetime2(6) NOT NULL,
  [LanguageID] int NULL,
  [parID] int NULL,
  [isGroup] int NOT NULL,
  [mainId] int NOT NULL,
  [isNotCountInGpa] bit NOT NULL,
  [CourseTypeDvoId] int NULL,
  [Hero_Subject_ID] int NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_Semesters', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_Semesters] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NOT NULL,
  [StartsOn] datetime2(6) NOT NULL,
  [EndsOn] datetime2(6) NOT NULL,
  [StudyYear] int NOT NULL,
  [SemesterTypeID] int NOT NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_SemesterTypes', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_SemesterTypes] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NOT NULL,
  [OrderBy] int NOT NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_ControlTypes', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_ControlTypes] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NULL,
  [ShortTitle] nvarchar(max) NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_CourseTypes', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_CourseTypes] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NOT NULL,
  [Code] nvarchar(max) NULL,
  [EctsCoefficient] real NOT NULL,
  [ShortTitle] nvarchar(max) NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_CourseTypeDvo', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_CourseTypeDvo] (
  [Id] int NOT NULL,
  [Title] nvarchar(max) NULL,
  PRIMARY KEY ([Id])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_Specializations', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_Specializations] (
  [Id] int NOT NULL,
  [TitleRu] nvarchar(max) NULL,
  [TitleKz] nvarchar(max) NULL,
  [TitleEn] nvarchar(max) NULL,
  [ShortTitleRu] nvarchar(max) NULL,
  [ShortTitleKz] nvarchar(max) NULL,
  [ShortTitleEn] nvarchar(max) NULL,
  [DescriptionRu] nvarchar(max) NULL,
  [DescriptionKz] nvarchar(max) NULL,
  [DescriptionEn] nvarchar(max) NULL,
  [EducationalProgramType] int NULL,
  [EducationalProgramStatus] int NULL,
  [IsEducationalProgram] bit NULL,
  [Code] nvarchar(max) NULL,
  [LevelId] int NULL,
  [RupEditorOrgUnitId] int NULL,
  [ChairId] int NULL,
  [Classifier] int NULL,
  [ESUVOID] int NULL,
  [NoBDID] nvarchar(max) NULL,
  PRIMARY KEY ([Id])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_SpecialitySpecializations', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_SpecialitySpecializations] (
  [ID] int NOT NULL,
  [SpecialityId] int NULL,
  [SpecializationId] int NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_Specializations_OrgUnits', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_Specializations_OrgUnits] (
  [SpecializationID] int NULL,
  [OrgUnitID] int NULL
);
END
GO

IF OBJECT_ID(N'dbo.Edu_Entrants', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_Entrants] (
  [EntrantID] int NOT NULL,
  [RegisteredOn] datetime2(6) NOT NULL,
  [LevelID] int NULL,
  [StatusID] int NOT NULL,
  [CheckedByAdmissions] bit NULL,
  [AdmissionsUserID] nvarchar(max) NULL,
  [SecretaryUserID] nvarchar(max) NULL,
  [AllowCheckByDoctor] bit NOT NULL,
  [CheckedByDoctor] bit NULL,
  [DoctorUserID] nvarchar(max) NULL,
  [CheckedByInstituteHead] bit NULL,
  [CheckedByDPOEmployee] bit NULL,
  [CheckedByDPOHead] bit NULL,
  [CheckedByOR] bit NULL,
  [ORUserID] nvarchar(max) NULL,
  [DocumentCheckTime] datetime2(6) NULL,
  [FormState] nvarchar(max) NULL,
  [LastUpdatedBy] nvarchar(max) NOT NULL,
  [LastUpdatedOn] datetime2(6) NOT NULL,
  [HasAppealation] bit NULL,
  [AppealReason] nvarchar(max) NULL,
  [Application] bit NULL,
  [HasReceipt] bit NULL,
  [HearAboutID] int NULL,
  [HearAboutText] nvarchar(max) NULL,
  [Choose] nvarchar(max) NULL,
  [AddressDocUploadTime] datetime2(6) NULL,
  [EnrollmentDate] datetime2(6) NULL,
  [DisabilityGroupID] int NULL,
  [RefLinkId] int NULL,
  [PreId] int NULL,
  [FilialCityID] int NULL,
  PRIMARY KEY ([EntrantID])
);
END
GO

IF OBJECT_ID(N'dbo.Edu_EntrantStatuses', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[Edu_EntrantStatuses] (
  [ID] int NOT NULL,
  [Title] nvarchar(max) NULL,
  PRIMARY KEY ([ID])
);
END
GO

IF OBJECT_ID(N'dbo.StudentInfo_Translations', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[StudentInfo_Translations] (
  [TableName] nvarchar(128) NOT NULL,
  [ColumnName] nvarchar(128) NOT NULL,
  [ObjectID] int NOT NULL,
  [Language] nvarchar(16) NOT NULL,
  [Value] nvarchar(max) NULL,
  CONSTRAINT [PK_StudentInfo_Translations] PRIMARY KEY ([TableName], [ColumnName], [ObjectID], [Language])
);
END
GO

IF OBJECT_ID(N'dbo.StudentInfo_Translations', N'U') IS NOT NULL
BEGIN
    DECLARE @StudentInfoTranslationsPk sysname;

    SELECT @StudentInfoTranslationsPk = kc.name
    FROM sys.key_constraints kc
    WHERE kc.parent_object_id = OBJECT_ID(N'dbo.StudentInfo_Translations')
      AND kc.[type] = 'PK';

    IF @StudentInfoTranslationsPk IS NOT NULL
    BEGIN
        EXEC(N'ALTER TABLE [dbo].[StudentInfo_Translations] DROP CONSTRAINT [' + @StudentInfoTranslationsPk + N']');
    END

    ALTER TABLE [dbo].[StudentInfo_Translations] ALTER COLUMN [TableName] nvarchar(128) NOT NULL;
    ALTER TABLE [dbo].[StudentInfo_Translations] ALTER COLUMN [ColumnName] nvarchar(128) NOT NULL;
    ALTER TABLE [dbo].[StudentInfo_Translations] ALTER COLUMN [Language] nvarchar(16) NOT NULL;
    ALTER TABLE [dbo].[StudentInfo_Translations] ADD CONSTRAINT [PK_StudentInfo_Translations] PRIMARY KEY ([TableName], [ColumnName], [ObjectID], [Language]);
END
GO

USE [EPVO_test];
GO

IF OBJECT_ID(N'dbo.PROFESSION', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[PROFESSION] (
  [Code] nvarchar(max) NULL,
  [Created] datetime2(6) NULL,
  [Deleted] int NULL,
  [DescriptionEn] nvarchar(max) NULL,
  [DescriptionKz] nvarchar(max) NULL,
  [DescriptionRu] nvarchar(max) NULL,
  [DoubleDiploma] bit NULL,
  [universityId] int NULL,
  [PartnerName] nvarchar(max) NULL,
  [ProfessionCode] nvarchar(max) NULL,
  [ProfessionId] int NOT NULL,
  [ProfessionNameEn] nvarchar(max) NULL,
  [ProfessionNameKz] nvarchar(max) NULL,
  [ProfessionNameRu] nvarchar(max) NULL,
  [Qualificationen] nvarchar(max) NULL,
  [Qualificationkz] nvarchar(max) NULL,
  [Qualificationru] nvarchar(max) NULL,
  [Classifier] int NULL,
  [TrainingDirectionsId] int NULL,
  [DdStart] date NULL,
  [AdvisorId] int NULL,
  [AccreditAgencyId] int NULL,
  [AccreditValidity] datetime2(6) NULL,
  [AccreditInstMark] nvarchar(max) NULL,
  [TypeCode] nvarchar(max) NULL,
  PRIMARY KEY ([ProfessionId])
);
END
GO

IF OBJECT_ID(N'dbo.STUDENT', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[STUDENT] (
  [UniversityId] int NULL,
  [StudentId] int NOT NULL,
  [FirstName] nvarchar(max) NULL,
  [LastName] nvarchar(max) NULL,
  [Patronymic] nvarchar(max) NULL,
  [BirthDate] date NULL,
  [StartDate] date NULL,
  [Address] nvarchar(max) NULL,
  [NationId] int NULL,
  [StudyFormId] int NULL,
  [StudyCalendarId] int NULL,
  [PaymentFormId] int NULL,
  [StudyLanguageId] int NULL,
  [Photo] varbinary(max) NULL,
  [ProfessionId] int NULL,
  [CourseNumber] int NULL,
  [TranscriptNumber] nvarchar(max) NULL,
  [TranscriptSeries] nvarchar(max) NULL,
  [IsMarried] int NULL,
  [IcNumber] nvarchar(max) NULL,
  [IcDate] date NULL,
  [Education] nvarchar(max) NULL,
  [HasExcellent] bit NULL,
  [StartOrder] nvarchar(max) NULL,
  [IsStudent] int NULL,
  [Certificate] nvarchar(max) NULL,
  [GrantNumber] nvarchar(max) NULL,
  [Gpa] decimal(18,2) NULL,
  [CurrentCreditsSum] real NULL,
  [Residence] int NULL,
  [SitizenshipId] int NULL,
  [DormState] int NULL,
  [IsInRetire] bit NULL,
  [FromId] int NULL,
  [Local] bit NULL,
  [City] nvarchar(max) NULL,
  [ContractId] int NULL,
  [SpecializationId] int NULL,
  [IinPlt] nvarchar(max) NULL,
  [AltynBelgi] bit NULL,
  [DataVydachiAttestata] date NULL,
  [DataVydachiDiploma] date NULL,
  [DateDocEducation] date NULL,
  [EndCollege] bit NULL,
  [EndHighSchool] bit NULL,
  [EndSchool] bit NULL,
  [IcSeries] nvarchar(max) NULL,
  [IcType] int NULL,
  [LivingAddress] nvarchar(max) NULL,
  [NomerAttestata] nvarchar(max) NULL,
  [OtherBirthPlace] nvarchar(max) NULL,
  [SeriesNumberDocEducation] nvarchar(max) NULL,
  [SeriyaAttestata] nvarchar(max) NULL,
  [SeriyaDiploma] nvarchar(max) NULL,
  [SchoolName] nvarchar(max) NULL,
  [FacultyId] int NULL,
  [SexId] int NULL,
  [Mail] nvarchar(max) NULL,
  [Phone] nvarchar(max) NULL,
  [SumPoints] int NULL,
  [SumPointsCreative] int NULL,
  [EnrollOrderDate] date NULL,
  [MobilePhone] nvarchar(max) NULL,
  [GrantType] int NULL,
  [AcademicMobility] int NULL,
  [IncorrectIin] bit NULL,
  [BirthPlaceCatoId] int NULL,
  [LivingPlaceCatoId] int NULL,
  [RegistrationPlaceCatoId] int NULL,
  [NaselennyiPunktAttestataCatoId] int NULL,
  [EnterExamType] int NULL,
  [FundingId] int NULL,
  [TypeCode] nvarchar(max) NULL,
  PRIMARY KEY ([StudentId])
);
END
GO

IF OBJECT_ID(N'dbo.STUDENT_INFO', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[STUDENT_INFO] (
  [UniversityId] int NOT NULL,
  [StudentId] int NOT NULL,
  [EndHighSchoolType] int NULL,
  [PreviousGpa] real NOT NULL,
  [EducationConditionId] int NOT NULL,
  [ForeignLangCertMark] nvarchar(max) NULL,
  [EntranceExamFinalMark] int NULL,
  [CenterUniversityId] int NULL,
  [CenterProfessionCode] nvarchar(max) NULL,
  [EntCertSeries] nvarchar(max) NULL,
  [EntCertDatePrint] date NULL,
  [EntPassedLang] int NULL,
  [EntIndividualCode] nvarchar(max) NULL,
  [ConditionallyEnrolled] bit NOT NULL,
  [GraduateDiplomaNumber] nvarchar(max) NULL,
  [GraduateDiplomaSeries] nvarchar(max) NULL,
  [NostrificationSeries] nvarchar(max) NULL,
  [NostrificationDate] date NULL,
  [OtherBornCountryId] int NULL,
  [BornInAnotherCountry] int NULL,
  [ForeignLangCertSubjectId] int NULL,
  [ExamBySpecialtySubjectId] int NULL,
  [ForeignLangCertExists] bit NOT NULL,
  [ForeignLangCertId] int NULL,
  [HighSchoolType] int NULL,
  [GraduatedCountryId] int NOT NULL,
  [ByProfile] bit NOT NULL,
  [DegreeId] int NOT NULL,
  [EntranceExamLanguageId] int NULL,
  [InstitutionId] int NULL,
  [IcDepartmentId] int NULL,
  [DomesticHighSchoolName] nvarchar(max) NULL,
  [DomesticHighSchoolProfession] nvarchar(max) NULL,
  [CertificateSeries] nvarchar(max) NULL,
  [AwardedDate] date NULL,
  [HasCreativeExam] bit NOT NULL,
  [SpecialExamProvided] bit NOT NULL,
  [SpecialExamAdmission] bit NOT NULL,
  [DomesticHighSchoolType] int NULL,
  [InterviewProtocolId] int NULL,
  [TrilingualEducation] bit NOT NULL,
  [InNationalStudentLeague] bit NOT NULL,
  [StudiedForeignLangId] nvarchar(max) NULL,
  [IntergovernmentalGrant] bit NOT NULL,
  [PublicAuthorityGrant] int NULL,
  [BenefitQuotaId] int NULL,
  [AddEntranceExamAdmission] int NULL,
  [AddEntranceExamDate] datetime2(6) NULL,
  [Iic] nvarchar(max) NULL,
  [Bic] nvarchar(max) NULL,
  [BankId] int NULL,
  [WinterAdmission] bit NOT NULL,
  [ProgramId] int NULL,
  [UpdateDate] date NULL,
  [FhighSchool] nvarchar(max) NULL,
  [FhighSchoolProfession] nvarchar(max) NULL,
  [TypeCode] nvarchar(max) NOT NULL,
  PRIMARY KEY ([UniversityId], [StudentId])
);
END
GO

IF OBJECT_ID(N'dbo.SCHOLARSHIP', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[SCHOLARSHIP] (
  [UniversityId] int NOT NULL,
  [Id] int NOT NULL,
  [StudentId] int NOT NULL,
  [ScholarshipYear] int NOT NULL,
  [ScholarshipMonth] int NOT NULL,
  [ScholarshipPayDate] date NULL,
  [ScholarshipMoney] real NULL,
  [ScholarshipTypeId] int NULL,
  [TerminationDate] date NULL,
  [AdditionalPayment] bit NULL,
  [SectionId] int NOT NULL,
  [ScholarshipAwardYear] int NOT NULL,
  [ScholarshipAwardTerm] int NOT NULL,
  [OverallPerformance] int NOT NULL,
  [TypeCode] nvarchar(max) NULL,
  PRIMARY KEY ([Id])
);
END
GO

IF OBJECT_ID(N'dbo.SCHOLARSHIP_NEW', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[SCHOLARSHIP_NEW] (
  [UniversityId] int NOT NULL,
  [Id] int NOT NULL,
  [StudentId] int NOT NULL,
  [ScholarshipYear] int NOT NULL,
  [ScholarshipMonth] int NOT NULL,
  [ScholarshipPayDate] date NULL,
  [ScholarshipMoney] real NULL,
  [ScholarshipTypeId] int NULL,
  [TerminationDate] date NULL,
  [AdditionalPayment] bit NULL,
  [SectionId] int NOT NULL,
  [ScholarshipAwardYear] int NOT NULL,
  [ScholarshipAwardTerm] int NOT NULL,
  [OverallPerformance] int NOT NULL,
  [TypeCode] nvarchar(max) NULL,
  PRIMARY KEY ([Id])
);
END
GO

IF OBJECT_ID(N'dbo.UNIVERSITY', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[UNIVERSITY] (
  [UniversityId] int NOT NULL,
  [FullNameEn] nvarchar(max) NULL,
  [FullNameKz] nvarchar(max) NULL,
  [FullNameRu] nvarchar(max) NULL,
  [AddressEn] nvarchar(max) NULL,
  [AddressKz] nvarchar(max) NULL,
  [AddressRu] nvarchar(max) NULL,
  [Phone] nvarchar(max) NULL,
  [RectorId] int NULL,
  [StartDate] date NULL,
  [CompCnt] int NULL,
  [OldCompCnt] int NULL,
  [StsOnComp] decimal(18,2) NULL,
  [UniversityTypeId] int NULL,
  [AccessRepLibrary] int NOT NULL,
  [SpeedInternetConn] nvarchar(max) NULL,
  [Bin] nvarchar(max) NULL,
  [RowNumberForPk] int NULL,
  [RegionId] int NULL,
  [RegionName] nvarchar(max) NULL,
  [Website] nvarchar(max) NULL,
  [StudyLanguages] nvarchar(max) NULL,
  [ComputerRoomsCount] int NULL,
  [LaboratoriesCount] int NULL,
  [BorrowingCheckSystem] int NULL,
  [BankId] int NULL,
  [BankAccNum] nvarchar(max) NULL,
  [Email] nvarchar(max) NULL,
  [CorpManOrgan] nvarchar(max) NULL,
  [HasMilitary] bit NULL,
  [PointsSpeedConnAbove] int NULL,
  [PointsSpeedConnUp] int NULL,
  [InfSystemId] int NULL,
  [InfSystemName] nvarchar(max) NULL,
  [InfSystemAddress] nvarchar(max) NULL,
  [CanteenCount] int NULL,
  [InternetCompCount] int NULL,
  [LibraryAddress] nvarchar(max) NULL,
  [TypeCode] nvarchar(max) NULL,
  PRIMARY KEY ([UniversityId])
);
END
GO

IF OBJECT_ID(N'dbo.SPECIALITIES_EPVO', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[SPECIALITIES_EPVO] (
  [UniversityId] real NULL,
  [Id] real NOT NULL,
  [profCafId] real NULL,
  [NameRu] nvarchar(max) NULL,
  [NameKz] nvarchar(max) NULL,
  [NameEn] nvarchar(max) NULL,
  [DoubleDiploma] bit NULL,
  [JointEp] bit NULL,
  [SpecializationCode] nvarchar(max) NULL,
  [StatusEp] real NULL,
  [EduProgType] real NULL,
  [Default] bit NULL,
  [Interdisciplinary] bit NULL,
  [EducationProgram] bit NULL,
  PRIMARY KEY ([Id])
);
END
GO

IF OBJECT_ID(N'dbo.SPECIALITIES_EPVO_2025', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[SPECIALITIES_EPVO_2025] (
  [UniversityId] nvarchar(max) NULL,
  [Id] nvarchar(255) NOT NULL,
  [ProfCafId] nvarchar(max) NULL,
  [NameRu] nvarchar(max) NULL,
  [DoubleDiploma] nvarchar(max) NULL,
  [JointEp] nvarchar(max) NULL,
  [UniversityType] nvarchar(max) NULL,
  [PartnerUniverId] nvarchar(max) NULL,
  [SpecializationCode] nvarchar(max) NULL,
  [StatusEp] nvarchar(max) NULL,
  [EduProgType] nvarchar(max) NULL,
  [IsEducationProgram] nvarchar(max) NULL,
  [ProfessionId] nvarchar(max) NULL,
  [IsInterdisciplinary] nvarchar(max) NULL,
  PRIMARY KEY ([Id])
);
END
GO

IF OBJECT_ID(N'dbo.SPECIALIZATIONS', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[SPECIALIZATIONS] (
  [Created] datetime2(6) NULL,
  [Deleted] datetime2(6) NULL,
  [Id] int NOT NULL,
  [UniversityId] int NULL,
  [Modified] datetime2(6) NULL,
  [NameEn] nvarchar(max) NULL,
  [NameKz] nvarchar(max) NULL,
  [NameRu] nvarchar(max) NULL,
  [ProfCafId] int NULL,
  [DescriptionEn] nvarchar(max) NULL,
  [DescriptionKz] nvarchar(max) NULL,
  [DescriptionRu] nvarchar(max) NULL,
  [DoubleDiploma] bit NULL,
  [EduProgType] int NULL,
  [IsEducationProgram] bit NULL,
  [JointEp] bit NULL,
  [PartnerName] nvarchar(max) NULL,
  [PartnerUniverId] int NULL,
  [SpecializationCode] nvarchar(max) NULL,
  [StatusEp] int NULL,
  [UniversityType] int NULL,
  [IsInterdisciplinary] bit NULL,
  [professionId] int NULL,
  [TypeCode] nvarchar(max) NULL,
  [IgnoreRms] bit NULL,
  [AcademicDegreeBaAwarded] bit NULL,
  PRIMARY KEY ([Id])
);
END
GO

IF OBJECT_ID(N'dbo.STUDY_FORMS', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[STUDY_FORMS] (
  [Id] int NOT NULL,
  [UniversityId] int NULL,
  [DegreeId] int NULL,
  [NameRu] nvarchar(max) NULL,
  [NameKz] nvarchar(max) NULL,
  [NameEn] nvarchar(max) NULL,
  [CourseCount] int NULL,
  [CreditsCount] int NULL,
  [TermsCount] int NULL,
  [DepartmentId] int NULL,
  [BaseEducationId] int NULL,
  [DistanceLearning] bit NULL,
  [TrainingCompletionTerm] int NULL,
  [InUse] bit NULL,
  [TypeCode] nvarchar(max) NULL,
  PRIMARY KEY ([Id])
);
END
GO

IF OBJECT_ID(N'dbo.STUDYCALENDAR', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[STUDYCALENDAR] (
  [StudyCalendarId] int NOT NULL,
  [UniversityId] int NULL,
  [Name] nvarchar(max) NULL,
  [StudyFormId] int NULL,
  [Year] int NULL,
  [CalendarTypeId] int NULL,
  [ProfessionId] int NULL,
  [SpecializationId] int NULL,
  [Status] int NULL,
  [EntranceYear] int NULL,
  [TypeCode] nvarchar(max) NULL,
  PRIMARY KEY ([StudyCalendarId])
);
END
GO

IF OBJECT_ID(N'dbo.FACULTIES', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[FACULTIES] (
  [Created] datetime2(6) NULL,
  [DialUp] int NULL,
  [FacultyDean] int NULL,
  [FacultyId] int NOT NULL,
  [FacultyNameEn] nvarchar(max) NULL,
  [FacultyNameKz] nvarchar(max) NULL,
  [FacultyNameRu] nvarchar(max) NULL,
  [UniversityId] int NULL,
  [InformationEn] nvarchar(max) NULL,
  [InformationKz] nvarchar(max) NULL,
  [InformationRu] nvarchar(max) NULL,
  [Proper] int NULL,
  [Satellite] int NULL,
  [TypeCode] nvarchar(max) NULL,
  PRIMARY KEY ([FacultyId])
);
END
GO

IF OBJECT_ID(N'dbo.CENTER_KATO', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[CENTER_KATO] (
  [Id] int NOT NULL,
  [Code] nvarchar(max) NULL,
  [NameRu] nvarchar(max) NULL,
  [NameKz] nvarchar(max) NULL,
  [FullNameRu] nvarchar(max) NULL,
  [FullNameKz] nvarchar(max) NULL,
  [Deep] int NULL,
  [UpdateDate] datetime2(6) NULL,
  [RegionCode] int NULL,
  [Status] int NULL,
  [OldCode] nvarchar(max) NULL,
  [UniversityId] int NULL,
  PRIMARY KEY ([Id])
);
END
GO

IF OBJECT_ID(N'dbo.center_countries', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[center_countries] (
  [Alfa2_Code] nvarchar(max) NULL,
  [Alfa3_Code] nvarchar(max) NULL,
  [Country_Code] nvarchar(max) NULL,
  [Full_NameEn] nvarchar(max) NULL,
  [Full_NameKz] nvarchar(max) NULL,
  [Full_NameRu] nvarchar(max) NULL,
  [Id] int NOT NULL,
  [Id_Regions] int NULL,
  [NameEn] nvarchar(max) NULL,
  [NameKz] nvarchar(max) NULL,
  [NameRu] nvarchar(max) NULL,
  [Update_Date] datetime2(6) NULL,
  PRIMARY KEY ([Id])
);
END
GO

IF OBJECT_ID(N'dbo.center_nationalities', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[center_nationalities] (
  [Id] int NOT NULL,
  [nameen] nvarchar(max) NULL,
  [namekz] nvarchar(max) NULL,
  [nameru] nvarchar(max) NULL,
  [Update_Date] datetime2(6) NULL,
  PRIMARY KEY ([Id])
);
END
GO

IF OBJECT_ID(N'dbo.MARITALSTATES', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[MARITALSTATES] (
  [Id] int NOT NULL,
  [NameEn] nvarchar(max) NULL,
  [NameKz] nvarchar(max) NULL,
  [NameRu] nvarchar(max) NULL,
  PRIMARY KEY ([Id])
);
END
GO

IF OBJECT_ID(N'dbo.NATIONALITIES', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[NATIONALITIES] (
  [Center_NationalitiesId] int NULL,
  [Id] int NOT NULL,
  [Id_University] int NULL,
  [NameEn] nvarchar(max) NULL,
  [NameKz] nvarchar(max) NULL,
  [NameRu] nvarchar(max) NULL,
  PRIMARY KEY ([Id])
);
END
GO

IF OBJECT_ID(N'dbo.STUDYLANGUAGES', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[STUDYLANGUAGES] (
  [Center_StudyLang_Id] int NULL,
  [Id] int NOT NULL,
  [Id_University] int NULL,
  [NameEn] nvarchar(max) NULL,
  [NameKz] nvarchar(max) NULL,
  [NameRu] nvarchar(max) NULL,
  PRIMARY KEY ([Id])
);
END
GO

IF OBJECT_ID(N'dbo.STUDENT_SSO', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[STUDENT_SSO] (
  [UniversityId] int NULL,
  [StudentId] int NOT NULL,
  [FirstName] nvarchar(max) NULL,
  [LastName] nvarchar(max) NULL,
  [Patronymic] nvarchar(max) NULL,
  [BirthDate] date NULL,
  [StartDate] date NULL,
  [Address] nvarchar(max) NULL,
  [NationId] int NULL,
  [StudyFormId] int NULL,
  [StudyCalendarId] int NULL,
  [PaymentFormId] int NULL,
  [StudyLanguageId] int NULL,
  [Photo] varbinary(max) NULL,
  [ProfessionId] int NULL,
  [CourseNumber] int NULL,
  [TranscriptNumber] nvarchar(max) NULL,
  [TranscriptSeries] nvarchar(max) NULL,
  [IsMarried] int NULL,
  [IcNumber] nvarchar(max) NULL,
  [IcDate] date NULL,
  [Education] nvarchar(max) NULL,
  [HasExcellent] bit NULL,
  [StartOrder] nvarchar(max) NULL,
  [IsStudent] int NULL,
  [Certificate] nvarchar(max) NULL,
  [GrantNumber] nvarchar(max) NULL,
  [Gpa] decimal(18,2) NULL,
  [CurrentCreditsSum] real NULL,
  [Residence] int NULL,
  [SitizenshipId] int NULL,
  [DormState] int NULL,
  [IsInRetire] bit NULL,
  [FromId] int NULL,
  [Local] bit NULL,
  [City] nvarchar(max) NULL,
  [ContractId] int NULL,
  [SpecializationId] int NULL,
  [IinPlt] nvarchar(max) NULL,
  [AltynBelgi] bit NULL,
  [DataVydachiAttestata] date NULL,
  [DataVydachiDiploma] date NULL,
  [DateDocEducation] date NULL,
  [EndCollege] bit NULL,
  [EndHighSchool] bit NULL,
  [EndSchool] bit NULL,
  [IcSeries] nvarchar(max) NULL,
  [IcType] int NULL,
  [LivingAddress] nvarchar(max) NULL,
  [NomerAttestata] nvarchar(max) NULL,
  [OtherBirthPlace] nvarchar(max) NULL,
  [SeriesNumberDocEducation] nvarchar(max) NULL,
  [SeriyaAttestata] nvarchar(max) NULL,
  [SeriyaDiploma] nvarchar(max) NULL,
  [SchoolName] nvarchar(max) NULL,
  [FacultyId] int NULL,
  [SexId] int NULL,
  [Mail] nvarchar(max) NULL,
  [Phone] nvarchar(max) NULL,
  [SumPoints] int NULL,
  [SumPointsCreative] int NULL,
  [EnrollOrderDate] date NULL,
  [MobilePhone] nvarchar(max) NULL,
  [GrantType] int NULL,
  [AcademicMobility] int NULL,
  [IncorrectIin] bit NULL,
  [BirthPlaceCatoId] int NULL,
  [LivingPlaceCatoId] int NULL,
  [RegistrationPlaceCatoId] int NULL,
  [NaselennyiPunktAttestataCatoId] int NULL,
  [EnterExamType] int NULL,
  [FundingId] int NULL,
  [TypeCode] nvarchar(max) NULL,
  PRIMARY KEY ([StudentId])
);
END
GO

IF OBJECT_ID(N'dbo.STUDENT_TEMP', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[STUDENT_TEMP] (
  [UniversityId] int NULL,
  [StudentId] int NOT NULL,
  [FirstName] nvarchar(max) NULL,
  [LastName] nvarchar(max) NULL,
  [Patronymic] nvarchar(max) NULL,
  [BirthDate] date NULL,
  [StartDate] date NULL,
  [Address] nvarchar(max) NULL,
  [NationId] int NULL,
  [StudyFormId] int NULL,
  [StudyCalendarId] int NULL,
  [PaymentFormId] int NULL,
  [StudyLanguageId] int NULL,
  [Photo] varbinary(max) NULL,
  [ProfessionId] int NULL,
  [CourseNumber] int NULL,
  [TranscriptNumber] nvarchar(max) NULL,
  [TranscriptSeries] nvarchar(max) NULL,
  [IsMarried] int NULL,
  [IcNumber] nvarchar(max) NULL,
  [IcDate] date NULL,
  [Education] nvarchar(max) NULL,
  [HasExcellent] bit NULL,
  [StartOrder] nvarchar(max) NULL,
  [IsStudent] int NULL,
  [Certificate] nvarchar(max) NULL,
  [GrantNumber] nvarchar(max) NULL,
  [Gpa] decimal(18,2) NULL,
  [CurrentCreditsSum] real NULL,
  [Residence] int NULL,
  [SitizenshipId] int NULL,
  [DormState] int NULL,
  [IsInRetire] bit NULL,
  [FromId] int NULL,
  [Local] bit NULL,
  [City] nvarchar(max) NULL,
  [ContractId] int NULL,
  [SpecializationId] int NULL,
  [IinPlt] nvarchar(max) NULL,
  [AltynBelgi] bit NULL,
  [DataVydachiAttestata] date NULL,
  [DataVydachiDiploma] date NULL,
  [DateDocEducation] date NULL,
  [EndCollege] bit NULL,
  [EndHighSchool] bit NULL,
  [EndSchool] bit NULL,
  [IcSeries] nvarchar(max) NULL,
  [IcType] int NULL,
  [LivingAddress] nvarchar(max) NULL,
  [NomerAttestata] nvarchar(max) NULL,
  [OtherBirthPlace] nvarchar(max) NULL,
  [SeriesNumberDocEducation] nvarchar(max) NULL,
  [SeriyaAttestata] nvarchar(max) NULL,
  [SeriyaDiploma] nvarchar(max) NULL,
  [SchoolName] nvarchar(max) NULL,
  [FacultyId] int NULL,
  [SexId] int NULL,
  [Mail] nvarchar(max) NULL,
  [Phone] nvarchar(max) NULL,
  [SumPoints] int NULL,
  [SumPointsCreative] int NULL,
  [EnrollOrderDate] date NULL,
  [MobilePhone] nvarchar(max) NULL,
  [GrantType] int NULL,
  [AcademicMobility] int NULL,
  [IncorrectIin] bit NULL,
  [BirthPlaceCatoId] int NULL,
  [LivingPlaceCatoId] int NULL,
  [RegistrationPlaceCatoId] int NULL,
  [NaselennyiPunktAttestataCatoId] int NULL,
  [EnterExamType] int NULL,
  [FundingId] int NULL,
  [TypeCode] nvarchar(max) NULL,
  PRIMARY KEY ([StudentId])
);
END
GO

IF OBJECT_ID(N'dbo.STUDENT_SYNC_LOG', N'U') IS NULL
BEGIN
CREATE TABLE [dbo].[STUDENT_SYNC_LOG] (
  [Id] int NOT NULL IDENTITY(1,1),
  [StudentId] int NOT NULL,
  [IinPlt] nvarchar(12) NULL,
  [SentAt] datetime2(6) NOT NULL,
  [Status] nvarchar(20) NOT NULL,
  [RequestBody] nvarchar(max) NULL,
  [ResponseBody] nvarchar(max) NULL,
  [ErrorMessage] nvarchar(max) NULL,
  [EpvoEndpoint] nvarchar(500) NULL,
  PRIMARY KEY ([Id])
);
END
GO

USE [KAZNITU_export];
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Users_NationalityID_Edu_Nationalities_1')
ALTER TABLE [dbo].[Edu_Users] ADD CONSTRAINT [FK_Edu_Users_NationalityID_Edu_Nationalities_1] FOREIGN KEY ([NationalityID]) REFERENCES [dbo].[Edu_Nationalities] ([ID]) ON DELETE SET NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Users_MaritalStatusID_Edu_MaritalStatuses_2')
ALTER TABLE [dbo].[Edu_Users] ADD CONSTRAINT [FK_Edu_Users_MaritalStatusID_Edu_MaritalStatuses_2] FOREIGN KEY ([MaritalStatusID]) REFERENCES [dbo].[Edu_MaritalStatuses] ([ID]) ON DELETE SET NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Users_MessengerTypeID_Edu_Messengers_3')
ALTER TABLE [dbo].[Edu_Users] ADD CONSTRAINT [FK_Edu_Users_MessengerTypeID_Edu_Messengers_3] FOREIGN KEY ([MessengerTypeID]) REFERENCES [dbo].[Edu_Messengers] ([ID]) ON DELETE SET NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Users_CitizenshipCountryID_Edu_Countries_4')
ALTER TABLE [dbo].[Edu_Users] ADD CONSTRAINT [FK_Edu_Users_CitizenshipCountryID_Edu_Countries_4] FOREIGN KEY ([CitizenshipCountryID]) REFERENCES [dbo].[Edu_Countries] ([ID]) ON DELETE SET NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Users_CitizenCategoryID_Edu_CitizenCategories_5')
ALTER TABLE [dbo].[Edu_Users] ADD CONSTRAINT [FK_Edu_Users_CitizenCategoryID_Edu_CitizenCategories_5] FOREIGN KEY ([CitizenCategoryID]) REFERENCES [dbo].[Edu_CitizenCategories] ([ID]) ON DELETE SET NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Students_StudentID_Edu_Users_1')
ALTER TABLE [dbo].[Edu_Students] ADD CONSTRAINT [FK_Edu_Students_StudentID_Edu_Users_1] FOREIGN KEY ([StudentID]) REFERENCES [dbo].[Edu_Users] ([ID]) ON DELETE CASCADE;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Students_EducationTypeID_Edu_EducationTypes_2')
ALTER TABLE [dbo].[Edu_Students] ADD CONSTRAINT [FK_Edu_Students_EducationTypeID_Edu_EducationTypes_2] FOREIGN KEY ([EducationTypeID]) REFERENCES [dbo].[Edu_EducationTypes] ([ID]) ON DELETE SET NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Students_EducationPaymentTypeID_EduPaymentTypes_3')
ALTER TABLE [dbo].[Edu_Students] ADD CONSTRAINT [FK_Edu_Students_EducationPaymentTypeID_EduPaymentTypes_3] FOREIGN KEY ([EducationPaymentTypeID]) REFERENCES [dbo].[Edu_EducationPaymentTypes] ([ID]) ON DELETE SET NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Students_GrantTypeID_Edu_GrantTypes_4')
ALTER TABLE [dbo].[Edu_Students] ADD CONSTRAINT [FK_Edu_Students_GrantTypeID_Edu_GrantTypes_4] FOREIGN KEY ([GrantTypeID]) REFERENCES [dbo].[Edu_GrantTypes] ([ID]) ON DELETE SET NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Students_EducationDurationID_Edu_EducationDurations_5')
ALTER TABLE [dbo].[Edu_Students] ADD CONSTRAINT [FK_Edu_Students_EducationDurationID_Edu_EducationDurations_5] FOREIGN KEY ([EducationDurationID]) REFERENCES [dbo].[Edu_EducationDurations] ([ID]) ON DELETE SET NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Students_StudyLanguageID_Edu_Languages_6')
ALTER TABLE [dbo].[Edu_Students] ADD CONSTRAINT [FK_Edu_Students_StudyLanguageID_Edu_Languages_6] FOREIGN KEY ([StudyLanguageID]) REFERENCES [dbo].[Edu_Languages] ([ID]) ON DELETE SET NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Students_AcademicStatusID_Edu_AcademicStatuses_7')
ALTER TABLE [dbo].[Edu_Students] ADD CONSTRAINT [FK_Edu_Students_AcademicStatusID_Edu_AcademicStatuses_7] FOREIGN KEY ([AcademicStatusID]) REFERENCES [dbo].[Edu_AcademicStatuses] ([ID]) ON DELETE SET NULL;
GO

IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Students_AdvisorID_Edu_Employees_8')
ALTER TABLE [dbo].[Edu_Students] DROP CONSTRAINT [FK_Edu_Students_AdvisorID_Edu_Employees_8];
GO

ALTER TABLE [dbo].[Edu_Students] ADD CONSTRAINT [FK_Edu_Students_AdvisorID_Edu_Employees_8] FOREIGN KEY ([AdvisorID]) REFERENCES [dbo].[Edu_Employees] ([ID]) ON DELETE NO ACTION;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Students_SpecialityID_Edu_Specialities_9')
ALTER TABLE [dbo].[Edu_Students] ADD CONSTRAINT [FK_Edu_Students_SpecialityID_Edu_Specialities_9] FOREIGN KEY ([SpecialityID]) REFERENCES [dbo].[Edu_Specialities] ([ID]) ON DELETE SET NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Students_RupID_Edu_Rups_10')
ALTER TABLE [dbo].[Edu_Students] ADD CONSTRAINT [FK_Edu_Students_RupID_Edu_Rups_10] FOREIGN KEY ([RupID]) REFERENCES [dbo].[Edu_Rups] ([ID]) ON DELETE SET NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Students_StatusID_Edu_StudentStatuses_11')
ALTER TABLE [dbo].[Edu_Students] ADD CONSTRAINT [FK_Edu_Students_StatusID_Edu_StudentStatuses_11] FOREIGN KEY ([StatusID]) REFERENCES [dbo].[Edu_StudentStatuses] ([ID]) ON DELETE SET NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Students_CategoryID_Edu_StudentCategories_12')
ALTER TABLE [dbo].[Edu_Students] ADD CONSTRAINT [FK_Edu_Students_CategoryID_Edu_StudentCategories_12] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[Edu_StudentCategories] ([ID]) ON DELETE SET NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Employees_ID_Edu_Users_1')
ALTER TABLE [dbo].[Edu_Employees] ADD CONSTRAINT [FK_Edu_Employees_ID_Edu_Users_1] FOREIGN KEY ([ID]) REFERENCES [dbo].[Edu_Users] ([ID]) ON DELETE CASCADE;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_EmployeePositions_EmployeeID_Edu_Employees_1')
ALTER TABLE [dbo].[Edu_EmployeePositions] ADD CONSTRAINT [FK_Edu_EmployeePositions_EmployeeID_Edu_Employees_1] FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[Edu_Employees] ([ID]) ON DELETE CASCADE;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_EmployeePositions_OrgUnitID_Edu_OrgUnits_2')
ALTER TABLE [dbo].[Edu_EmployeePositions] ADD CONSTRAINT [FK_Edu_EmployeePositions_OrgUnitID_Edu_OrgUnits_2] FOREIGN KEY ([OrgUnitID]) REFERENCES [dbo].[Edu_OrgUnits] ([ID]) ON DELETE CASCADE;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_EmployeePositions_PositionID_Edu_Positions_3')
ALTER TABLE [dbo].[Edu_EmployeePositions] ADD CONSTRAINT [FK_Edu_EmployeePositions_PositionID_Edu_Positions_3] FOREIGN KEY ([PositionID]) REFERENCES [dbo].[Edu_Positions] ([ID]) ON DELETE CASCADE;
GO

IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_EducationDurations_LevelID_Edu_SpecialityLevels_1')
ALTER TABLE [dbo].[Edu_EducationDurations] DROP CONSTRAINT [FK_Edu_EducationDurations_LevelID_Edu_SpecialityLevels_1];
GO

ALTER TABLE [dbo].[Edu_EducationDurations] ADD CONSTRAINT [FK_Edu_EducationDurations_LevelID_Edu_SpecialityLevels_1] FOREIGN KEY ([LevelID]) REFERENCES [dbo].[Edu_SpecialityLevels] ([ID]) ON DELETE NO ACTION;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Positions_CategoryID_Edu_PositionCategories_1')
ALTER TABLE [dbo].[Edu_Positions] ADD CONSTRAINT [FK_Edu_Positions_CategoryID_Edu_PositionCategories_1] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[Edu_PositionCategories] ([ID]) ON DELETE SET NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_OrgUnits_TypeID_Edu_OrgUnitTypes_1')
ALTER TABLE [dbo].[Edu_OrgUnits] ADD CONSTRAINT [FK_Edu_OrgUnits_TypeID_Edu_OrgUnitTypes_1] FOREIGN KEY ([TypeID]) REFERENCES [dbo].[Edu_OrgUnitTypes] ([ID]) ON DELETE CASCADE;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_OrgUnits_ParentID_Edu_OrgUnits_2')
ALTER TABLE [dbo].[Edu_OrgUnits] ADD CONSTRAINT [FK_Edu_OrgUnits_ParentID_Edu_OrgUnits_2] FOREIGN KEY ([ParentID]) REFERENCES [dbo].[Edu_OrgUnits] ([ID]) ON DELETE NO ACTION;
GO

IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Specialities_LevelID_Edu_SpecialityLevels_1')
ALTER TABLE [dbo].[Edu_Specialities] DROP CONSTRAINT [FK_Edu_Specialities_LevelID_Edu_SpecialityLevels_1];
GO

ALTER TABLE [dbo].[Edu_Specialities] ADD CONSTRAINT [FK_Edu_Specialities_LevelID_Edu_SpecialityLevels_1] FOREIGN KEY ([LevelID]) REFERENCES [dbo].[Edu_SpecialityLevels] ([ID]) ON DELETE NO ACTION;
GO

IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Specialities_PrimarySubjectID_Edu_SchoolSubjects_2')
ALTER TABLE [dbo].[Edu_Specialities] DROP CONSTRAINT [FK_Edu_Specialities_PrimarySubjectID_Edu_SchoolSubjects_2];
GO

ALTER TABLE [dbo].[Edu_Specialities] ADD CONSTRAINT [FK_Edu_Specialities_PrimarySubjectID_Edu_SchoolSubjects_2] FOREIGN KEY ([PrimarySubjectID]) REFERENCES [dbo].[Edu_SchoolSubjects] ([ID]) ON DELETE NO ACTION;
GO

IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Specialities_FithSubjectID_Edu_SchoolSubjects_3')
ALTER TABLE [dbo].[Edu_Specialities] DROP CONSTRAINT [FK_Edu_Specialities_FithSubjectID_Edu_SchoolSubjects_3];
GO

ALTER TABLE [dbo].[Edu_Specialities] ADD CONSTRAINT [FK_Edu_Specialities_FithSubjectID_Edu_SchoolSubjects_3] FOREIGN KEY ([FithSubjectID]) REFERENCES [dbo].[Edu_SchoolSubjects] ([ID]) ON DELETE NO ACTION;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Specialities_RupEditorOrgUnitID_Edu_OrgUnits_4')
ALTER TABLE [dbo].[Edu_Specialities] ADD CONSTRAINT [FK_Edu_Specialities_RupEditorOrgUnitID_Edu_OrgUnits_4] FOREIGN KEY ([RupEditorOrgUnitID]) REFERENCES [dbo].[Edu_OrgUnits] ([ID]) ON DELETE SET NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_UserDocuments_UserID_Edu_Users_1')
ALTER TABLE [dbo].[Edu_UserDocuments] ADD CONSTRAINT [FK_Edu_UserDocuments_UserID_Edu_Users_1] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Edu_Users] ([ID]) ON DELETE CASCADE;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_UserDocuments_DocumentTypeID_Edu_UserDocumentTypes_2')
ALTER TABLE [dbo].[Edu_UserDocuments] ADD CONSTRAINT [FK_Edu_UserDocuments_DocumentTypeID_Edu_UserDocumentTypes_2] FOREIGN KEY ([DocumentTypeID]) REFERENCES [dbo].[Edu_UserDocumentTypes] ([ID]) ON DELETE NO ACTION;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_UserDocuments_IssuedByID_Edu_DocumentIssueOrgs_3')
ALTER TABLE [dbo].[Edu_UserDocuments] ADD CONSTRAINT [FK_Edu_UserDocuments_IssuedByID_Edu_DocumentIssueOrgs_3] FOREIGN KEY ([IssuedByID]) REFERENCES [dbo].[Edu_DocumentIssueOrgs] ([ID]) ON DELETE SET NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Rups_AlgorithmID_Edu_RupAlgorithms_1')
ALTER TABLE [dbo].[Edu_Rups] ADD CONSTRAINT [FK_Edu_Rups_AlgorithmID_Edu_RupAlgorithms_1] FOREIGN KEY ([AlgorithmID]) REFERENCES [dbo].[Edu_RupAlgorithms] ([ID]) ON DELETE SET NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Rups_EducationDurationID_Edu_EducationDurations_2')
ALTER TABLE [dbo].[Edu_Rups] ADD CONSTRAINT [FK_Edu_Rups_EducationDurationID_Edu_EducationDurations_2] FOREIGN KEY ([EducationDurationID]) REFERENCES [dbo].[Edu_EducationDurations] ([ID]) ON DELETE SET NULL;
GO

IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Rups_SpecialityID_Edu_Specialities_3')
ALTER TABLE [dbo].[Edu_Rups] DROP CONSTRAINT [FK_Edu_Rups_SpecialityID_Edu_Specialities_3];
GO

ALTER TABLE [dbo].[Edu_Rups] ADD CONSTRAINT [FK_Edu_Rups_SpecialityID_Edu_Specialities_3] FOREIGN KEY ([SpecialityID]) REFERENCES [dbo].[Edu_Specialities] ([ID]) ON DELETE NO ACTION;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Localities_TypeID_Edu_LocalityTypes_1')
ALTER TABLE [dbo].[Edu_Localities] ADD CONSTRAINT [FK_Edu_Localities_TypeID_Edu_LocalityTypes_1] FOREIGN KEY ([TypeID]) REFERENCES [dbo].[Edu_LocalityTypes] ([ID]) ON DELETE CASCADE;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Localities_ParentID_Edu_Localities_2')
ALTER TABLE [dbo].[Edu_Localities] ADD CONSTRAINT [FK_Edu_Localities_ParentID_Edu_Localities_2] FOREIGN KEY ([ParentID]) REFERENCES [dbo].[Edu_Localities] ([ID]) ON DELETE NO ACTION;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_UserAddresses_UserID_Edu_Users_1')
ALTER TABLE [dbo].[Edu_UserAddresses] ADD CONSTRAINT [FK_Edu_UserAddresses_UserID_Edu_Users_1] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Edu_Users] ([ID]) ON DELETE CASCADE;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_UserAddresses_AddressTypeID_Edu_AddressTypes_2')
ALTER TABLE [dbo].[Edu_UserAddresses] ADD CONSTRAINT [FK_Edu_UserAddresses_AddressTypeID_Edu_AddressTypes_2] FOREIGN KEY ([AddressTypeID]) REFERENCES [dbo].[Edu_AddressTypes] ([ID]) ON DELETE CASCADE;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_UserAddresses_CountryID_Edu_Countries_3')
ALTER TABLE [dbo].[Edu_UserAddresses] ADD CONSTRAINT [FK_Edu_UserAddresses_CountryID_Edu_Countries_3] FOREIGN KEY ([CountryID]) REFERENCES [dbo].[Edu_Countries] ([ID]) ON DELETE CASCADE;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_UserAddresses_LocalityID_Edu_Localities_4')
ALTER TABLE [dbo].[Edu_UserAddresses] ADD CONSTRAINT [FK_Edu_UserAddresses_LocalityID_Edu_Localities_4] FOREIGN KEY ([LocalityID]) REFERENCES [dbo].[Edu_Localities] ([ID]) ON DELETE CASCADE;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_UserEducation_UserID_Edu_Users_1')
ALTER TABLE [dbo].[Edu_UserEducation] ADD CONSTRAINT [FK_Edu_UserEducation_UserID_Edu_Users_1] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Edu_Users] ([ID]) ON DELETE CASCADE;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_UserEducation_DocumentTypeID_Edu_EducationDocumentTypes_2')
ALTER TABLE [dbo].[Edu_UserEducation] ADD CONSTRAINT [FK_Edu_UserEducation_DocumentTypeID_Edu_EducationDocumentTypes_2] FOREIGN KEY ([DocumentTypeID]) REFERENCES [dbo].[Edu_EducationDocumentTypes] ([ID]) ON DELETE NO ACTION;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_UserEducation_DocumentSubTypeID_EduDocSubTypes_3')
ALTER TABLE [dbo].[Edu_UserEducation] ADD CONSTRAINT [FK_Edu_UserEducation_DocumentSubTypeID_EduDocSubTypes_3] FOREIGN KEY ([DocumentSubTypeID]) REFERENCES [dbo].[Edu_EducationDocumentSubTypes] ([ID]) ON DELETE SET NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_UserEducation_StudyLanguageID_Edu_Languages_4')
ALTER TABLE [dbo].[Edu_UserEducation] ADD CONSTRAINT [FK_Edu_UserEducation_StudyLanguageID_Edu_Languages_4] FOREIGN KEY ([StudyLanguageID]) REFERENCES [dbo].[Edu_Languages] ([ID]) ON DELETE SET NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_UserEducation_SpecialityID_Edu_Specialities_5')
ALTER TABLE [dbo].[Edu_UserEducation] ADD CONSTRAINT [FK_Edu_UserEducation_SpecialityID_Edu_Specialities_5] FOREIGN KEY ([SpecialityID]) REFERENCES [dbo].[Edu_Specialities] ([ID]) ON DELETE SET NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Schools_SchoolTypeID_Edu_SchoolTypes_1')
ALTER TABLE [dbo].[Edu_Schools] ADD CONSTRAINT [FK_Edu_Schools_SchoolTypeID_Edu_SchoolTypes_1] FOREIGN KEY ([SchoolTypeID]) REFERENCES [dbo].[Edu_SchoolTypes] ([ID]) ON DELETE CASCADE;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Schools_SchoolRegionStatusID_Edu_SchoolRegionStatuses_2')
ALTER TABLE [dbo].[Edu_Schools] ADD CONSTRAINT [FK_Edu_Schools_SchoolRegionStatusID_Edu_SchoolRegionStatuses_2] FOREIGN KEY ([SchoolRegionStatusID]) REFERENCES [dbo].[Edu_SchoolRegionStatuses] ([ID]) ON DELETE CASCADE;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Schools_LocalityID_Edu_Localities_3')
ALTER TABLE [dbo].[Edu_Schools] ADD CONSTRAINT [FK_Edu_Schools_LocalityID_Edu_Localities_3] FOREIGN KEY ([LocalityID]) REFERENCES [dbo].[Edu_Localities] ([ID]) ON DELETE SET NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_StudentCourses_StudentID_Edu_Students_1')
ALTER TABLE [dbo].[Edu_StudentCourses] ADD CONSTRAINT [FK_Edu_StudentCourses_StudentID_Edu_Students_1] FOREIGN KEY ([StudentID]) REFERENCES [dbo].[Edu_Students] ([StudentID]) ON DELETE CASCADE;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_StudentCourses_SemesterCourseID_Edu_SemesterCourses_2')
ALTER TABLE [dbo].[Edu_StudentCourses] ADD CONSTRAINT [FK_Edu_StudentCourses_SemesterCourseID_Edu_SemesterCourses_2] FOREIGN KEY ([SemesterCourseID]) REFERENCES [dbo].[Edu_SemesterCourses] ([ID]) ON DELETE CASCADE;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_StudentCourses_LevelID_Edu_SpecialityLevels_3')
ALTER TABLE [dbo].[Edu_StudentCourses] ADD CONSTRAINT [FK_Edu_StudentCourses_LevelID_Edu_SpecialityLevels_3] FOREIGN KEY ([LevelID]) REFERENCES [dbo].[Edu_SpecialityLevels] ([ID]) ON DELETE SET NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_SemesterCourses_SemesterID_Edu_Semesters_1')
ALTER TABLE [dbo].[Edu_SemesterCourses] ADD CONSTRAINT [FK_Edu_SemesterCourses_SemesterID_Edu_Semesters_1] FOREIGN KEY ([SemesterID]) REFERENCES [dbo].[Edu_Semesters] ([ID]) ON DELETE CASCADE;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_SemesterCourses_OrgUnitID_Edu_OrgUnits_2')
ALTER TABLE [dbo].[Edu_SemesterCourses] ADD CONSTRAINT [FK_Edu_SemesterCourses_OrgUnitID_Edu_OrgUnits_2] FOREIGN KEY ([OrgUnitID]) REFERENCES [dbo].[Edu_OrgUnits] ([ID]) ON DELETE CASCADE;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_SemesterCourses_ControlTypeID_Edu_ControlTypes_3')
ALTER TABLE [dbo].[Edu_SemesterCourses] ADD CONSTRAINT [FK_Edu_SemesterCourses_ControlTypeID_Edu_ControlTypes_3] FOREIGN KEY ([ControlTypeID]) REFERENCES [dbo].[Edu_ControlTypes] ([ID]) ON DELETE CASCADE;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_SemesterCourses_CourseTypeID_Edu_CourseTypes_4')
ALTER TABLE [dbo].[Edu_SemesterCourses] ADD CONSTRAINT [FK_Edu_SemesterCourses_CourseTypeID_Edu_CourseTypes_4] FOREIGN KEY ([CourseTypeID]) REFERENCES [dbo].[Edu_CourseTypes] ([ID]) ON DELETE CASCADE;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_SemesterCourses_LanguageID_Edu_Languages_5')
ALTER TABLE [dbo].[Edu_SemesterCourses] ADD CONSTRAINT [FK_Edu_SemesterCourses_LanguageID_Edu_Languages_5] FOREIGN KEY ([LanguageID]) REFERENCES [dbo].[Edu_Languages] ([ID]) ON DELETE SET NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_SemesterCourses_CourseTypeDvoId_Edu_CourseTypeDvo_6')
ALTER TABLE [dbo].[Edu_SemesterCourses] ADD CONSTRAINT [FK_Edu_SemesterCourses_CourseTypeDvoId_Edu_CourseTypeDvo_6] FOREIGN KEY ([CourseTypeDvoId]) REFERENCES [dbo].[Edu_CourseTypeDvo] ([Id]) ON DELETE SET NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Semesters_SemesterTypeID_Edu_SemesterTypes_1')
ALTER TABLE [dbo].[Edu_Semesters] ADD CONSTRAINT [FK_Edu_Semesters_SemesterTypeID_Edu_SemesterTypes_1] FOREIGN KEY ([SemesterTypeID]) REFERENCES [dbo].[Edu_SemesterTypes] ([ID]) ON DELETE CASCADE;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Entrants_EntrantID_Edu_Users_1')
ALTER TABLE [dbo].[Edu_Entrants] ADD CONSTRAINT [FK_Edu_Entrants_EntrantID_Edu_Users_1] FOREIGN KEY ([EntrantID]) REFERENCES [dbo].[Edu_Users] ([ID]) ON DELETE CASCADE;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Entrants_LevelID_Edu_SpecialityLevels_2')
ALTER TABLE [dbo].[Edu_Entrants] ADD CONSTRAINT [FK_Edu_Entrants_LevelID_Edu_SpecialityLevels_2] FOREIGN KEY ([LevelID]) REFERENCES [dbo].[Edu_SpecialityLevels] ([ID]) ON DELETE SET NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Edu_Entrants_StatusID_Edu_EntrantStatuses_3')
ALTER TABLE [dbo].[Edu_Entrants] ADD CONSTRAINT [FK_Edu_Entrants_StatusID_Edu_EntrantStatuses_3] FOREIGN KEY ([StatusID]) REFERENCES [dbo].[Edu_EntrantStatuses] ([ID]) ON DELETE CASCADE;
GO
