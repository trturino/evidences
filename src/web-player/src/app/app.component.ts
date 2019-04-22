import { Component, OnInit, HostListener } from '@angular/core';
import { SignalrService } from './services/signalr-service/signalr.service';
import { PlayerService } from './services/player-service/player.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  nowPlaying: any;
  queue = [];
  songId: string;

  constructor(private signalR: SignalrService, private playerService: PlayerService) {  }

  ngOnInit() {
    this.signalR.OnAddSong.subscribe(x => this.onSongAdded(x));
    this.signalR.OnReaction.subscribe(x => this.onReaction(x));
    this.signalR.OnRemoveSong.subscribe(x => this.onSongRemoved(x));

    this.signalR.connect();
  }

  fullScreen() {
    const elem: any = document.documentElement;
    const methodToBeInvoked = elem.requestFullscreen ||
      elem.webkitRequestFullScreen || elem.mozRequestFullscreen
      ||  elem.msRequestFullscreen;
    if (methodToBeInvoked) {
      methodToBeInvoked.call(elem);
    }
  }

  onSongAdded(data) {
    this.queue.push(data);
    this.playerService.playVideo(data);
    console.log(this.songId);
    console.log(data);
  }

  onSongRemoved(data) {

  }

  onReaction(data) {

  }

  @HostListener('window:resize', ['$event'])
  onResize(event) {
    this.playerService.setSize(window.innerHeight, window.innerWidth);
  }
}
