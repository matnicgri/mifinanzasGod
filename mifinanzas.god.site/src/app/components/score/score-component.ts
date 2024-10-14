import { Component, Input, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms'; 
import { CommonModule } from '@angular/common';
import { Score } from '../../models/score-model';
import { GameService } from '../../services/game-service';

@Component({
  selector: 'score-component',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './score-component.html',
  styleUrls: ['./score-component.css']
})
export class ScoreComponent {
  @Input() gameId: number | null = null; 
  title = 'Score';  
  scores: Score[] = []; 

  constructor(private gameService: GameService) {}


  ngOnInit() {        
      this.loadScores();

      this.gameService.currentGameStatus.subscribe(gameId => {
          this.loadScores(); 
      });
  } 

  loadScores() {
    if (this.gameId !== null) {
      this.gameService.GetScores(this.gameId).subscribe(
        {
          next: response => {
            if (response.success) {
              this.scores = response.data; // Asignar los puntajes a la variable
              console.log('Scores retrieved successfully:', this.scores);        
            } else {
              console.error('Error retrieving scores:', response.error);
              alert("Error retrieving scores: " + response.error);
            }
          },
          error: error => {
            console.error('Error retrieving scores:', error);
            alert("Error retrieving scores.");
          }
        }
      );
    }else{
      alert("No se cargò un id de partida.");
    }
  }  

}
