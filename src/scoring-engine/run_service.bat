@echo off
cd /d "c:\Projects\SmartLoan360-with-sln\SmartLoan360\src\scoring-engine"
C:/Projects/SmartLoan360-with-sln/SmartLoan360/.venv/Scripts/python.exe -m uvicorn main:app --host 0.0.0.0 --port 8000 --reload
pause
