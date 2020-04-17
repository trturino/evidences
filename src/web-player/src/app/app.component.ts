import { Component, OnInit, HostListener } from '@angular/core';
import { SignalrService } from './services/signalr-service/signalr.service';
import { PlayerService } from './services/player-service/player.service';
import { ApiService } from './services/api-service/api.service';
import { Queue } from './components/queue/Queue';


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

  constructor(
    private signalR: SignalrService,
    private playerService: PlayerService,
    private apiService: ApiService) {
    this.playerService.songHasFinished.subscribe(() => this.onSongFinished());
  }

  ngOnInit() {
    this.signalR.OnAddSong.subscribe(x => this.onSongAdded(x));
    this.signalR.OnReaction.subscribe(x => this.onReaction(x));
    this.signalR.OnRemoveSong.subscribe(x => this.onSongRemoved(x));
    this.setState();

    this.signalR.connect();
  }

  fullScreen() {
    const elem: any = document.documentElement;
    const methodToBeInvoked = elem.requestFullscreen ||
      elem.webkitRequestFullScreen || elem.mozRequestFullscreen
      || elem.msRequestFullscreen;
    if (methodToBeInvoked) {
      methodToBeInvoked.call(elem);
    }
    this.isHackVisible = false;
  }

  async setState() {
    this.apiService.getState().subscribe((state: any) => {
      if (state.queue && state.queue.length > 0) {
        for (const song of state.queue) {
          this.queue.push(song);
        }
      }
      const nextSong = this.queue[0];
      this.nextSong = nextSong;
    });
  }

  animate() {
    this.emojis = ['🤑', '💩', '🤩', '😫', '😁', '😍', '🎤'];
  }

  onSongAdded(data) {
    if (this.queue.length === 0) {
      this.nextSong = data;
    }
    this.queue.push(data);
  }

  onSongFinished() {
    this.nowPlaying = undefined;
    this.stageOpened = false;
    this.apiService.stopPlaying().subscribe(x => console.log(x));

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
    this.queue = this.queue.filter(x => x.id !== this.nextSong.id);

    setTimeout(() => {
      this.stageOpened = true;
      this.apiService.startPlaying(this.nextSong.id).subscribe(x => console.log(x));
      this.playerService.playVideo(this.nextSong);
      if (this.queue.length > 0) {
        this.nextSong = this.queue[0];
      } else {
        this.nextSong = null;
      }
    }, 1000);

    this.showScore = false;
  }

  onReaction(data) {
    this.emojis.push(data.reaction);
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
