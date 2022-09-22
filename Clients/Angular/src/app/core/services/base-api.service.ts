import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@budgetify/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BaseApiService {
  public httpHeaders: HttpHeaders = new HttpHeaders({
    'Content-Type': 'application/json',
    Accept: 'application/json'
  });

  constructor(private httpClient: HttpClient) {}

  public get<T>(url: string, params: HttpParams = new HttpParams()): Observable<T> {
    return this.httpClient.get<T>(`${environment.baseApiUrl}${url}`, { headers: this.httpHeaders, params });
  }

  public post<T>(url: string, data: object = {}, httpHeaders?: HttpHeaders): Observable<T> {
    return this.httpClient.post<T>(`${environment.baseApiUrl}${url}`, JSON.stringify(data), {
      headers: httpHeaders ? httpHeaders : this.httpHeaders
    });
  }

  public put<T>(url: string, data: object = {}): Observable<T> {
    return this.httpClient.put<T>(`${environment.baseApiUrl}${url}`, JSON.stringify(data), {
      headers: this.httpHeaders
    });
  }

  public delete<T>(url: string): Observable<T> {
    return this.httpClient.delete<T>(`${environment.baseApiUrl}${url}`, { headers: this.httpHeaders });
  }

  public patch<T>(url: string, data: object = {}): Observable<T> {
    return this.httpClient.patch<T>(`${environment.baseApiUrl}${url}`, JSON.stringify(data), {
      headers: this.httpHeaders
    });
  }
}
