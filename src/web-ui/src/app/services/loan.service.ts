import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

export interface LoanRequest {
  fullName: string;
  amount: number;
  termMonths: number;
}

export interface LoanResponse {
  message: string;
  approvedAmount: number;
  applicationId: string;
}

@Injectable({ providedIn: 'root' })
export class LoanService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  applyLoan(request: LoanRequest) {
    return this.http.post<LoanResponse>(`${this.apiUrl}/api/apply`, request);
  }

  scoreLoan(request: LoanRequest) {
    return this.http.post<any>(`${this.apiUrl}/api/score`, request);
  }
}
