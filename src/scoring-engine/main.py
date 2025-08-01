from fastapi import FastAPI
from pydantic import BaseModel
from typing import Literal

app = FastAPI()

class ScoreRequest(BaseModel):
    fullName: str
    amount: float
    termMonths: int

class ScoreResponse(BaseModel):
    score: float
    risk: Literal["low", "medium", "high"]

@app.post("/score", response_model=ScoreResponse)
def score_loan(data: ScoreRequest):
    # Mock scoring logic
    if data.amount < 5000:
        risk = "low"
        score = 0.9
    elif data.amount < 15000:
        risk = "medium"
        score = 0.6
    else:
        risk = "high"
        score = 0.3
    return ScoreResponse(score=score, risk=risk)

if __name__ == "__main__":
    import uvicorn
    uvicorn.run("main:app", host="0.0.0.0", port=8000, reload=True)
