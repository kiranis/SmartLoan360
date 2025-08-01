import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

export interface LoanApplication {
  fullName: string;
  amount: number;
  termMonths: number;
}

export interface LoanResult {
  message: string;
  approvedAmount: number;
}

export interface ScoreResult {
  score: number;
  risk: string;
}

@Injectable({ providedIn: 'root' })
export class LoanService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  applyForLoan(application: LoanApplication) {
    return this.http.post<LoanResult>(`${this.apiUrl}/api/apply`, application);
  }

  scoreLoan(application: LoanApplication) {
    return this.http.post<ScoreResult>(`${this.apiUrl}/api/score`, application);
  }
}
