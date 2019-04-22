import { Injectable, NgZone } from '@angular/core';
import { ApiService } from '../api-service/api.service';

@Injectable({
  providedIn: 'root'
})
export class PlayerService {
  player: YT.Player;
  nowPlaying: any;

  constructor(private zone: NgZone, private apiService: ApiService) { }

  setupPlayer(player) {
    this.player = player;
  }

  changePlayerState(event) {
    const state = event.data;
    console.log(event);
    // let autoNext = false;
    // play the next song if its not the end of the playlist
    // should add a "repeat" feature
    if (state === YT.PlayerState.ENDED) {
      this.apiService.stopPlaying();
      this.nowPlaying = undefined;
    }

    if (state === YT.PlayerState.PAUSED) {
      // service.playerState = YT.PlayerState.PAUSED;
    }
    if (state === YT.PlayerState.PLAYING) {
      this.apiService.startPlaying(this.nowPlaying);
    }
  }

  setSize(height, width) {
    this.zone.runOutsideAngular(() => {
      this.player.setSize(width, height);
    });
  }

  playVideo(song: any, seconds?: number) {
    this.nowPlaying = song;
    const id = song.Url.replace('http://www.youtube.com/watch?v=', '');
    const isLoaded = this.player.getVideoUrl().includes(id);
    if (!isLoaded) {
      this.zone.runOutsideAngular(() =>
        this.player.loadVideoById(id, seconds || undefined)
      );
    }
  }

  play() {
    this.zone.runOutsideAngular(() => this.player.playVideo());
  }
}
