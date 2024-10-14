import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms'; 
import { Router } from '@angular/router';
import { GameService } from '../../services/game-service'; // Importa el servicio
import { Observable } from 'rxjs';
import { Game } from '../../models/game-model';

@Component({
  selector: 'start-component',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './start-component.html',
  styleUrls: ['./start-component.css']
})
export class StartComponent {
  title = 'Enters player\'s names';
  txtPlayer1: string = '';
  txtPlayer2: string = '';

  constructor(private router: Router,private gameService: GameService) {} // Inyecta Router
  
  onSubmit() {
    const playerId = 1; //TO DO obtener desde ajax luego de dar de alta la partida
    console.log('Player 1:', this.txtPlayer1);
    console.log('Player 2:', this.txtPlayer2);

    const gameData: Game = { // Usa el modelo aquí
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
        alert("Error creando partida.");
      }
    });

    
  }
}
