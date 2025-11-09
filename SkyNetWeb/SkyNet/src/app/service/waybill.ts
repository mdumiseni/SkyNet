import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { WaybillDetails } from 'src/app/waybill-details';

@Injectable({
  providedIn: 'root'
})
export class WaybillService {
  private apiUrl = 'http://localhost:5000/waybills';

  constructor(private http: HttpClient) { }

  getWaybillByNumber(number: string): Observable<WaybillDetails> {
    return this.http.get<WaybillDetails>(`${this.apiUrl}/${number}`);
  }
}