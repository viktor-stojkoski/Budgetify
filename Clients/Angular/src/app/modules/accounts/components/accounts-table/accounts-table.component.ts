import { Component, OnInit } from '@angular/core';
import { IAccountResponse } from '../../models/account.model';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-accounts-table',
  templateUrl: './accounts-table.component.html',
  styleUrls: ['./accounts-table.component.scss']
})
export class AccountsTableComponent implements OnInit {
  public accounts: IAccountResponse[] | undefined;

  constructor(private accountService: AccountService) {}

  public ngOnInit(): void {
    this.getAccounts();
  }

  private getAccounts(): void {
    this.accountService.getAccounts().subscribe({
      next: (response) => (this.accounts = response),
      error: (error) => console.error(error)
    });
  }
}
