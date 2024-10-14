import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from '../../enviroments/environment';
import { ApiResponse } from '../models/api-response-model';
import { Game } from '../models/game-model';
import { Score } from '../models/score-model';
import { MoveIn } from '../models/move-in-model';
import { Status } from '../models/status-model';
import { Movement } from '../models/movement-model';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GameService {  
  private apiUrl = environment.apiUrl;
  private gameStatusSource = new BehaviorSubject<number | null>(null);
  public currentGameStatus = this.gameStatusSource.asObservable();

  constructor(private http: HttpClient) {}

  createGame(gameData: Game): Observable<ApiResponse<number>> {
    return this.http.post<any>(`${this.apiUrl}/game`, gameData);    
  }

  Move(moveData:MoveIn): Observable<ApiResponse<Status>> {
    return this.http.post<any>(`${this.apiUrl}/movements`, moveData);    
  }

  GetScores(gameId: number): Observable<ApiResponse<Score[]>> {
    return this.http.get<any>(`${this.apiUrl}/game/score?gameId=${gameId}`);    
  }

  updateGameStatus(gameId: number) {
    this.gameStatusSource.next(gameId);
  }

  GetMovementOptions(): Observable<ApiResponse<Movement[]>> {
    let id=-1;
    return this.http.get<any>(`${this.apiUrl}/movements/options?id=${id}`);    
  }

  UpdateMovementOptions(mov:any): Observable<ApiResponse<boolean>> {
    return this.http.post<any>(`${this.apiUrl}/movements/options`, mov);    
  }
}
