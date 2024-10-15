import { Routes } from '@angular/router';
import { StartComponent } from './components/start-game/start-component';
import { GameComponent } from './components/game/game-component';

export const routes: Routes = [
  { path: '', component: StartComponent }, 
  { path: 'game/:id', component: GameComponent } 
];
