import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { IAccountResponse } from '../../models/account.model';
import { AccountService } from '../../services/account.service';
import { TranslationKeys } from '../../static/translationKeys';
import { CreateAccountComponent } from '../create-account/create-account.component';

@Component({
  selector: 'app-accounts-table',
  templateUrl: './accounts-table.component.html',
  styleUrls: ['./accounts-table.component.scss']
})
export class AccountsTableComponent implements OnInit {
  public accounts: IAccountResponse[] | undefined;
  public displayedColumns: string[] = ['name', 'type', 'balance', 'description'];
  public readonly translationKeys = TranslationKeys;
  public isLoading = true;

  constructor(private accountService: AccountService, private dialog: MatDialog) {}

  public ngOnInit(): void {
    this.getAccounts();
  }

  public openCreateAccountDialog() {
    this.dialog
      .open(CreateAccountComponent, {
        width: '600px'
      })
      .afterClosed()
      .subscribe({
        // TODO: switch map ?
        next: () => this.getAccounts()
      });
  }

  private getAccounts(): void {
    this.accountService.getAccounts().subscribe({
      next: (response) => {
        this.accounts = response.value;
        this.isLoading = false;
      },
      error: (error) => {
        console.error(error);
        this.isLoading = false;
      }
    });
  }
}
