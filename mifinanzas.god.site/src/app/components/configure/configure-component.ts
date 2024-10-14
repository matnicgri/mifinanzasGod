import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms'; 
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Game } from '../../models/game-model';
import {Movement} from '../../models/movement-model'
import {GameService} from '../../services/game-service'
import { CommonModule } from '@angular/common';

@Component({
  selector: 'configure-component',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './configure-component.html',
  styleUrls: ['./configure-component.css']
})
export class ConfigureComponent {
  title = 'Configure moves';
  moveOptions: Movement[] = [];
  selectedKillIds: { [key: number]: number | null } = {}; 
  selectedMove: number | null = null;
  maxMovements: number = 10;
  newMovementDescription: string = "";

  constructor(private router: Router,private gameService: GameService) {} 
  
  ngOnInit() {   
    this.chargeMoveOptions();    
  }

  chargeMoveOptions(){
    this.gameService.GetMovementOptions().subscribe(
      {
        next: response => {
          if (response.success) {
            this.moveOptions=response.data;
            this.moveOptions.forEach(move => {
              this.selectedKillIds[move.id] = move.killId; 
            });
          } else {            
            console.error('Error retrieving movements options:', response.error);
            alert("Error retrieving movements options: " + response.error);
          }
        }
      });
  }

  getDescriptionByKillId(killId: number): string {
    const move = this.moveOptions.find(m => m.id === killId);
    return move ? move.description : 'Unknown';
  }

  onSubmit() {
    const selectedMoves: Movement[] = this.moveOptions.map(move => {
      const selectedKillId = this.selectedKillIds[move.id]??-1; 
      return {
          id: move.id, 
          description: move.description || "", 
          killId: +selectedKillId
      };
    });
    
    const moves={moveOptions:selectedMoves};
    this.gameService.UpdateMovementOptions(moves).subscribe({
      next: response => {
          if (!response.success) {
              console.error('Error updating movements options:', response.error);
              alert("Error updating movements options: " + response.error);
          } else {
              alert("Movements updated successfully!"); 
          }
      },
      error: err => {
          console.error('Error occurred while updating movements:', err); 
          alert("An error occurred while updating movements.");
      }
  });   
  }

  addMovement() {
    if (this.moveOptions.length < this.maxMovements && this.newMovementDescription.trim() !== "") {
      const newId = this.moveOptions.length + 1; 
      this.moveOptions.push({ id: newId, description: this.newMovementDescription, killId: 1 }); 
      this.selectedKillIds[newId] = 1; 
      this.newMovementDescription = ""; 
    } else if (this.moveOptions.length >= this.maxMovements) {
      alert("Maximum of 10 movements reached.");
    } else {
      alert("Please enter a description for the new movement.");
    }
  }
}
