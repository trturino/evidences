import { Component, OnInit, HostListener, Input, Output, NgZone } from '@angular/core';
import { PlayerService } from 'src/app/services/player-service/player.service';

@Component({
  selector: 'app-player',
  templateUrl: './app-player.component.html',
  styleUrls: ['./app-player.component.scss']
})
export class AppPlayerComponent implements OnInit {

  screenHeight: number;
  screenWidth: number;

  constructor(private playerService: PlayerService, private zone: NgZone) {
    this.getScreenSize();
  }

  ngOnInit() {
  }

  setupPlayer(player) {
    this.playerService.setupPlayer(player);
  }

  updatePlayerState(event) {
    this.playerService.changePlayerState(event);
  }

  getScreenSize(event?) {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;
  }

}
