import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { enviroment } from '../../enviroments/enviroment';
import { ApiResponse } from '../models/api-response-model';
import { Game } from '../models/game-model';
import { Score } from '../models/score-model';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GameService {  
  private apiUrl = enviroment.apiUrl;
  private gameStatusSource = new BehaviorSubject<number | null>(null);
  public currentGameStatus = this.gameStatusSource.asObservable();

  constructor(private http: HttpClient) {}

  createGame(gameData: Game): Observable<ApiResponse<number>> {
    return this.http.post<any>(`${this.apiUrl}/game`, gameData);    
  }

  GetScores(gameId: number): Observable<ApiResponse<Score[]>> {
    return this.http.get<any>(`${this.apiUrl}/game/score?gameId=${gameId}`);    
  }

  updateGameStatus(gameId: number) {
    this.gameStatusSource.next(gameId);
  }

}
