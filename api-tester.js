const http = require("http");
const fs = require("fs");
const path = require("path");

// ─── Config ─────────────────────────────────────────────────────────────────
const BASE_URL = "http://localhost:5150";
const LOG_FILE = path.join(__dirname, "api-test-errors.txt");
const DATA_FILE = path.join(__dirname, "api-test-data.txt");

// ─── Endpoints ───────────────────────────────────────────────────────────────
const EPVO_SSO_ENDPOINTS = [
  "professions",
  "students",
  "students-info",
  "scholarships",
  "scholarships-new",
  "universities",
  "specialities",
  "specialities-new",
  "specializations",
  "study-forms",
  "study-calendars",
  "faculties",
  "center-kato",
  "center-countries",
  "center-nationalities",
  "maritalstates",
  "nationalities",
  "study-languages",
  "students-temp",
  "students-dump",
  "students-sso",
  "student-change-logs",
].map((e) => ({ label: `EpvoSso :: ${e}`, url: `${BASE_URL}/api/epvo-sso/${e}` }));

const UNIVERSITY_ENDPOINTS = [
  "academic-statuses",
  "address-types",
  "citizen-categories",
  "control-types",
  "countries",
  "course-types",
  "course-type-dvo",
  "document-issue-orgs",
  "education-document-subtypes",
  "education-document-types",
  "education-durations",
  "education-payment-types",
  "education-types",
  "employee-positions",
  "employees",
  "entrant-statuses",
  "entrants",
  "grant-types",
  "languages",
  "localities",
  "locality-types",
  "marital-statuses",
  "messengers",
  "nationalities",
  "org-units",
  "org-unit-types",
  "position-categories",
  "positions",
  "regions",
  "rup-algorithms",
  "rups",
  "school-region-statuses",
  "schools",
  "school-subjects",
  "school-types",
  "semester-courses",
  "semesters",
  "semester-types",
  "specialities",
  "speciality-levels",
  "speciality-specializations",
  "specializations",
  "specializations-org-units",
  "student-categories",
  "student-courses",
  "student-statuses",
  "students",
  "student-info-translations",
  "user-addresses",
  "user-document-types",
  "user-documents",
  "user-education",
  "users",
  "scollarship-students-info",
].map((e) => ({ label: `University :: ${e}`, url: `${BASE_URL}/api/university/${e}` }));

const ALL_ENDPOINTS = [...EPVO_SSO_ENDPOINTS, ...UNIVERSITY_ENDPOINTS];

// ─── Logger ──────────────────────────────────────────────────────────────────
const logStream = fs.createWriteStream(LOG_FILE, { flags: "a" });
const dataStream = fs.createWriteStream(DATA_FILE, { flags: "a" });

function logError(message) {
  const line = `[${new Date().toISOString()}] ${message}`;
  console.error("  ❌ " + message);
  logStream.write(line + "\n");
}

function logInfo(message) {
  console.log("  ✅ " + message);
}

function saveResponseData(label, url, body) {
  const header = `\n${"=".repeat(70)}\n[${new Date().toISOString()}] API: ${label}\nURL: ${url}\n${"─".repeat(70)}`;
  dataStream.write(header + "\n");
  dataStream.write(body + "\n");
}

// ─── HTTP request ─────────────────────────────────────────────────────────────
function fetchEndpoint(url) {
  return new Promise((resolve) => {
    const start = Date.now();
    const req = http.get(url, { timeout: 600000 }, (res) => {
      const elapsed = Date.now() - start;
      let body = "";
      res.on("data", (chunk) => (body += chunk));
      res.on("end", () => resolve({ status: res.statusCode, elapsed, body }));
    });
    req.on("timeout", () => {
      req.destroy();
      resolve({ status: null, elapsed: 600000, error: "Request timeout (10m)" });
    });
    req.on("error", (err) => {
      resolve({ status: null, elapsed: Date.now() - start, error: err.message });
    });
  });
}

// ─── Main ─────────────────────────────────────────────────────────────────────
async function main() {
  const sessionHeader = `\n${"=".repeat(70)}\nAPI Test Run: ${new Date().toISOString()}\n${"=".repeat(70)}`;
  logStream.write(sessionHeader + "\n");
  console.log(sessionHeader);

  let passed = 0;
  let failed = 0;

  for (const { label, url } of ALL_ENDPOINTS) {
    process.stdout.write(`\nTesting [${label}] ... `);
    const result = await fetchEndpoint(url);

    if (result.error) {
      failed++;
      logError(`FAIL  | ${label} | URL: ${url} | Error: ${result.error}`);
    } else if (result.status >= 200 && result.status < 300) {
      passed++;
      logInfo(`${result.status}  | ${label} | ${result.elapsed}ms`);
      saveResponseData(label, url, result.body);
    } else {
      failed++;
      // Trim body to first 300 chars for readability
      const snippet = result.body?.slice(0, 300).replace(/\n/g, " ") || "(empty)";
      logError(
        `FAIL  | ${label} | Status: ${result.status} | ${result.elapsed}ms | URL: ${url} | Body: ${snippet}`
      );
    }
  }

  const summary = `\n${"─".repeat(70)}\nTotal: ${ALL_ENDPOINTS.length} | Passed: ${passed} | Failed: ${failed}\n${"─".repeat(70)}`;
  console.log(summary);
  logStream.write(summary + "\n");

  logStream.end();
  dataStream.end();

  if (failed > 0) {
    console.log(`\n⚠️  Ошибки записаны в файл: ${LOG_FILE}`);
  } else {
    console.log(`\n🎉 Все ${passed} endpoint-ов прошли успешно!`);
  }
  console.log(`\n📄 Данные ответов записаны в файл: ${DATA_FILE}`);
}

main().catch((err) => {
  console.error("Критическая ошибка:", err);
  logStream.write(`[${new Date().toISOString()}] CRITICAL: ${err.message}\n`);
  logStream.end();
});
