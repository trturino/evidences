export interface IAppPlayer {
  index: number;
  media?: any;
  showPlayer: boolean;
  playerState: number;
  fullscreen: {
    on: boolean;
    height: number;
    width: number;
  };
  isFullscreen: boolean;
}
