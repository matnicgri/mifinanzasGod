import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { enviroment } from '../../enviroments/enviroment';
import { ApiResponse } from '../models/api-response-model';
import { MoveIn } from '../models/move-in-model';
import { Status } from '../models/status-model';
import { Movement } from '../models/movement-model';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MovementsService {  
  private apiUrl = enviroment.apiUrl;
  private gameStatusSource = new BehaviorSubject<number | null>(null);
  public currentGameStatus = this.gameStatusSource.asObservable();

  constructor(private http: HttpClient) {}

  Move(moveData:MoveIn): Observable<ApiResponse<Status>> {
    return this.http.post<any>(`${this.apiUrl}/movements`, moveData);    
  }

  GetMovementOptions(): Observable<ApiResponse<Movement[]>> {
    let id=-1;
    return this.http.get<any>(`${this.apiUrl}/movements/options?id=${id}`);    
  }

  UpdateMovementOptions(mov:any): Observable<ApiResponse<boolean>> {
    return this.http.post<any>(`${this.apiUrl}/movements/options`, mov);    
  }
}
