import { ConfettiRibbon } from './confettiRibbon';
import { ConfettiPaper } from './confettiPaper';
import { Vector2 } from './vector2';

export class ConfettiAnimation {
  confettiRibbonCount = 4;
  confettiPaperCount = 195;
  ribbonPaperCount = 30;
  ribbonPaperDist = 8.0;
  ribbonPaperThick = 8.0;
  speed = 30;
  duration = (1.0 / this.speed);

  canvasParent: any;
  canvasWidth: number;
  canvasHeight: number;
  canvas: HTMLCanvasElement;
  confettiRibbons: Array<ConfettiRibbon>;
  confettiPapers: Array<ConfettiPaper>;
  private readonly context: CanvasRenderingContext2D;
  interval: number;
  isStopping = true;

  constructor(id: string) {
    this.canvas = document.getElementById(id) as HTMLCanvasElement;
    this.canvasParent = this.canvas.parentNode;
    this.canvasWidth = this.canvasParent.offsetWidth;
    this.canvasHeight = this.canvasParent.offsetHeight;
    this.canvas.width = this.canvasWidth * window.devicePixelRatio;
    this.canvas.height = this.canvasHeight * window.devicePixelRatio;
    this.context = this.canvas.getContext('2d');
    this.confettiRibbons = new Array();
    ConfettiRibbon.bounds = new Vector2(this.canvasWidth, this.canvasHeight);
    for (let i = 0; i < this.confettiRibbonCount; i++) {
      const ribbonY = - Math.random() * this.canvasHeight / 4;
      this.confettiRibbons[i] = new ConfettiRibbon(Math.random() * this.canvasWidth,
        ribbonY, this.ribbonPaperCount, this.ribbonPaperDist, this.ribbonPaperThick, 45, 1, 0.05);
    }
    this.confettiPapers = new Array();
    ConfettiPaper.bounds = new Vector2(this.canvasWidth, this.canvasHeight);
    for (let i = 0; i < this.confettiPaperCount; i++) {
      this.confettiPapers[i] = new ConfettiPaper(Math.random() * this.canvasWidth, - Math.random() * this.canvasHeight / 5);
    }
  }

  Update() {
    this.context.clearRect(0, 0, this.canvas.width, this.canvas.height);
    let animationHasEnded = true;

    for (let i = 0; i < this.confettiPaperCount; i++) {
      const confettiPaper = this.confettiPapers[i];
      confettiPaper.Update(this.duration);
      confettiPaper.Draw(this.context);

      if (confettiPaper.pos.y < ConfettiPaper.bounds.y + confettiPaper.size) {
        animationHasEnded = false;
      }
    }
    for (let i = 0; i < this.confettiRibbonCount; i++) {
      const confettiRibbon = this.confettiRibbons[i];
      confettiRibbon.Update(this.duration);
      confettiRibbon.Draw(this.context);

      if (confettiRibbon.position.y < ConfettiRibbon.bounds.y + confettiRibbon.particleDist * confettiRibbon.particleCount) {
        animationHasEnded = false;
      }
    }

    if (animationHasEnded) {
      this.Stop();
    } else {
      this.interval = window.requestAnimationFrame(() => this.Update());
    }
  }

  Start() {
    this.Update();
  }

  Stop() {
    window.cancelAnimationFrame(this.interval);
  }

  Resize() {
    this.canvasWidth = this.canvasParent.offsetWidth;
    this.canvasHeight = this.canvasParent.offsetHeight;
    this.canvas.width = this.canvasWidth * window.devicePixelRatio;
    this.canvas.height = this.canvasHeight * window.devicePixelRatio;
    ConfettiPaper.bounds = new Vector2(this.canvasWidth, this.canvasHeight);
    ConfettiRibbon.bounds = new Vector2(this.canvasWidth, this.canvasHeight);
  }
}
