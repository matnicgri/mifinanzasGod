import { Player } from "./player-model";

export interface Status {
  gameFinished:boolean;
  gamePlayerWinner: string; 
  nextRoundId:number;
  players: Player[];
}