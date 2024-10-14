import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms'; 
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { GameService } from '../../services/game-service'; // Importa el servicio
import { Game } from '../../models/game-model';
import { ConfigureComponent } from '../configure/configure-component';

@Component({
  selector: 'start-component',
  standalone: true,
  imports: [CommonModule, FormsModule, ConfigureComponent],
  templateUrl: './start-component.html',
  styleUrls: ['./start-component.css']
})
export class StartComponent {
  title = 'Enter player\'s names';
  txtPlayer1: string = '';
  txtPlayer2: string = '';

  constructor(private router: Router, private gameService: GameService) {}

  onSubmit() {
    const gameData: Game = {
      player1Name: this.txtPlayer1,
      player2Name: this.txtPlayer2,
      gameId: -1
    };

    this.gameService.createGame(gameData).subscribe({
      next: response => {
        console.log('Game created successfully:', response);
        this.router.navigate(['/game', response.data]);
      },
      error: error => {
        console.error('Error creating game:', error);
        alert("Error creating game.");
      }
    });
  }
}
