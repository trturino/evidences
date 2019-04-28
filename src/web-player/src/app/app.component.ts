import { Component, OnInit, HostListener } from '@angular/core';
import { SignalrService } from './services/signalr-service/signalr.service';
import { PlayerService } from './services/player-service/player.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],

})
export class AppComponent implements OnInit {

  nowPlaying: any;
  queue = [];
  songId: string;
  nextSong: any;

  showLogo = true;
  showNextSong = true;

  stageOpened = false;
  showScore = false;
  isHackVisible = false;

  emojis = [];

  constructor(private signalR: SignalrService, private playerService: PlayerService) {
    this.playerService.songHasFinished.subscribe(() => this.onSongFinished());
   }

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
    this.isHackVisible = false;
  }

  animate() {
    this.emojis = ['🤑', '💩', '🤩', '😫', '😁', '😍', '🎤'];
  }

  onSongAdded(data) {
    this.nextSong = data;
    this.queue.push(data);
  }

  onSongFinished() {
    this.nowPlaying = null;
    this.stageOpened = false;

    setTimeout(() => {
      this.showScore = true;
      setTimeout(() => {
        this.showNextSong = true;
      }, 3000);
    }, 2000);
  }

  onSongRemoved(data) {

  }

  playSong() {
    this.showNextSong = false;
    this.showLogo = false;

    setTimeout(() => {
      this.stageOpened = true;
      this.playerService.playVideo(this.nextSong);
      this.nextSong = null;
    }, 1000);

    this.showScore = false;
  }

  onReaction(data) {
    this.emojis.push(data.Reaction);
  }

  stage() {
    this.stageOpened = !this.stageOpened;
  }

  score() {
    this.showScore = !this.showScore;
  }

  @HostListener('window:resize', ['$event'])
  onResize(event) {
    this.playerService.setSize(window.innerHeight, window.innerWidth);
  }
}
