import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms'; 
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import {ScoreComponent} from '../score/score-component'
import {MoveIn} from '../../models/move-in-model'
import {Player} from '../../models/player-model'
import {Movement} from '../../models/movement-model'
import {GameService} from '../../services/game-service'
import { Router } from '@angular/router';

@Component({
  selector: 'game-component',
  standalone: true,
  imports: [FormsModule,CommonModule,ScoreComponent],
  templateUrl: './game-component.html',
  styleUrls: ['./game-component.css']
})
export class GameComponent {
  id: number = -1;

  currentRound: number=1;
  title = 'Round 1';
  txtPlayerName: string = '';
  currentPlayer: Player | undefined;
  moveOptions: Movement[] | null = null;
  selectedMove: number | null = null;
  gameFinished: boolean=false;
  gameWinner: string="";

  constructor(private route: ActivatedRoute, private router: Router, private gameService: GameService) {}

  ngOnInit() {   
    this.route.params.subscribe(params => {
      this.id = params['id']; 
      console.log('Game ID:', this.id); 
    });

    this.chargeMoveOptions();

    //el primer movimiento es por defecto lo resuelve el backend
    const moveData: MoveIn = { 
      gameId: this.id,
      roundId: this.currentRound,
      playerId: -1,
      moveOptionId: -1
    };
    this.Move(moveData);
  }

  chargeMoveOptions(){
    this.gameService.GetMovementOptions().subscribe(
      {
        next: response => {
          if (response.success) {
            this.moveOptions=response.data;
          } else {            
            console.error('Error retrieving movements options:', response.error);
            alert("Error retrieving movements options: " + response.error);
          }
        }
      });
  }

  Move(moveData: MoveIn){

    this.gameService.Move(moveData).subscribe(
      {
        next: response => {
          if (response.success) {
            let result=response.data;
            this.currentRound = result.nextRoundId;    
            this.gameFinished=result.gameFinished;
            this.gameWinner=result.gamePlayerWinner;  
            
            this.currentPlayer = result.players.find(player => player.turn === true);
        
            if (this.currentPlayer) {
              this.txtPlayerName = this.currentPlayer.name;
            } else {
              alert("No player is currently in turn.");
            }

            const currentPlayerWinner = result.players.find(player => player.winner === true);
            if (currentPlayerWinner) {
              this.gameService.updateGameStatus(this.id);
            }

          } else {
            console.error('Error retrieving moves:', response.error);
            alert("Error retrieving moves: " + response.error);
          }
        },
        error: error => {
          console.error('Error retrieving moves:', error);
          alert("Error retrieving moves.");
        }
      }
    );
  }

  onSubmit() {
    const moveData: MoveIn = { 
      gameId: this.id,
      roundId: this.currentRound,
      playerId: this.currentPlayer ? this.currentPlayer.id : -1,
      moveOptionId: this.selectedMove ? this.selectedMove : -1,
    };
    this.Move(moveData);  
  }

  playAgain(){
    this.router.navigate(['/']);
  }
}
