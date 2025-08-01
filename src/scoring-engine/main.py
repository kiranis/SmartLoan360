from fastapi import FastAPI
from pydantic import BaseModel
from typing import Literal

app = FastAPI(title="Loan Scoring Engine", version="1.0.0")

class ScoreRequest(BaseModel):
    fullName: str
    amount: float
    termMonths: int

class ScoreResponse(BaseModel):
    score: float
    risk: Literal["low", "medium", "high"]

@app.post("/score", response_model=ScoreResponse)
def score_loan(data: ScoreRequest):
    if data.amount < 5000:
        return ScoreResponse(score=0.9, risk="low")
    elif data.amount < 15000:
        return ScoreResponse(score=0.6, risk="medium")
    else:
        return ScoreResponse(score=0.3, risk="high")

if __name__ == "__main__":
    import uvicorn
    uvicorn.run("main:app", host="0.0.0.0", port=8000, reload=True)
