import { Injectable } from '@angular/core';
import { BaseApiService } from '@budgetify/core';
import { IResult } from '@budgetify/shared';
import { Observable } from 'rxjs';
import {
  ICategoryResponse,
  ICreateMerchantRequest,
  IMerchantResponse,
  IUpdateMerchantRequest
} from '../models/merchant.model';

@Injectable({
  providedIn: 'root'
})
export class MerchantService {
  private merchantsApiRoute = 'merchants';
  private categoriesApiRoute = 'categories';
  constructor(private baseApiService: BaseApiService) {}

  public getMerchants(): Observable<IResult<IMerchantResponse[]>> {
    return this.baseApiService.get<IResult<IMerchantResponse[]>>(this.merchantsApiRoute);
  }

  public getMerchant(uid: string | null): Observable<IResult<IMerchantResponse>> {
    return this.baseApiService.get<IResult<IMerchantResponse>>(`${this.merchantsApiRoute}/${uid}`);
  }

  public createMerchant(request: ICreateMerchantRequest): Observable<void> {
    return this.baseApiService.post<void>(this.merchantsApiRoute, request);
  }

  public getCategories(): Observable<IResult<ICategoryResponse[]>> {
    return this.baseApiService.get<IResult<ICategoryResponse[]>>(this.categoriesApiRoute);
  }

  public updateMerchant(uid: string | null, request: IUpdateMerchantRequest): Observable<void> {
    return this.baseApiService.put<void>(`${this.merchantsApiRoute}/${uid}`, request);
  }
}
