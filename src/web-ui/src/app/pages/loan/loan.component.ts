import { Component } from '@angular/core';
import { LoanService, LoanRequest } from '../../services/loan.service';

@Component({
  selector: 'app-loan',
  templateUrl: './loan.component.html'
})
export class LoanComponent {
  fullName = '';
  amount = 0;
  termMonths = 0;
  result: any = null;
  error = '';

  constructor(private loanService: LoanService) {}

  submit() {
    if (!this.fullName || !this.amount || !this.termMonths) {
      this.error = 'Please fill in all fields';
      return;
    }

    const loanRequest: LoanRequest = {
      fullName: this.fullName,
      amount: this.amount,
      termMonths: this.termMonths
    };

    this.loanService.applyLoan(loanRequest).subscribe({
      next: (response) => {
        this.result = response;
        this.error = '';
      },
      error: () => {
        this.error = 'Failed to submit loan application';
        this.result = null;
      }
    });
  }
}
