import { Vector2 } from './vector2';
import { colors, DEG_TO_RAD } from './constants';
import { EulerMass } from './eulerMass';

export class ConfettiRibbon {

  public static bounds = new Vector2(0, 0);

  particleDist: number;
  particleCount: number;
  particleDrag: number;
  particleMass: number;
  particles: Array<EulerMass>;
  frontColor: string;
  backColor: string;
  xOff: number;
  yOff: number;
  position: Vector2;
  prevPosition: Vector2;
  velocityInherit: number;
  time: number;
  oscillationSpeed: number;
  oscillationDistance: number;
  ySpeed: number;

  constructor(x, y, count, dist, thickness, angle, mass, drag) {
    this.particleDist = dist;
    this.particleCount = count;
    this.particleMass = mass;
    this.particleDrag = drag;
    this.particles = new Array();
    const ci = Math.round(Math.random() * (colors.length - 1));
    this.frontColor = colors[ci][0];
    this.backColor = colors[ci][1];
    this.xOff = (Math.cos(DEG_TO_RAD * angle) * thickness);
    this.yOff = (Math.sin(DEG_TO_RAD * angle) * thickness);
    this.position = new Vector2(x, y);
    this.prevPosition = new Vector2(x, y);
    this.velocityInherit = (Math.random() * 2 + 4);
    this.time = Math.random() * 100;
    this.oscillationSpeed = (Math.random() * 2 + 2);
    this.oscillationDistance = (Math.random() * 40 + 40);
    this.ySpeed = (Math.random() * 40 + 80);
    for (let i = 0; i < this.particleCount; i++) {
      this.particles[i] = new EulerMass(x, y - i * this.particleDist, this.particleMass, this.particleDrag);
    }
  }

  Update(dt) {
    this.time += dt * this.oscillationSpeed;
    this.position.y += this.ySpeed * dt;
    this.position.x += Math.cos(this.time) * this.oscillationDistance * dt;
    this.particles[0].position = this.position;
    const dX = this.prevPosition.x - this.position.x;
    const dY = this.prevPosition.y - this.position.y;
    const delta = Math.sqrt(dX * dX + dY * dY);
    this.prevPosition = new Vector2(this.position.x, this.position.y);
    for (let i = 1; i < this.particleCount; i++) {
      const dirP = Vector2.Sub(this.particles[i - 1].position, this.particles[i].position);
      dirP.Normalize();
      dirP.Mul((delta / dt) * this.velocityInherit);
      this.particles[i].AddForce(dirP);
    }
    for (let i = 1; i < this.particleCount; i++) {
      this.particles[i].Integrate(dt);
    }
    for (let i = 1; i < this.particleCount; i++) {
      const rp2 = new Vector2(this.particles[i].position.x, this.particles[i].position.y);
      rp2.Sub(this.particles[i - 1].position);
      rp2.Normalize();
      rp2.Mul(this.particleDist);
      rp2.Add(this.particles[i - 1].position);
      this.particles[i].position = rp2;
    }
  }

  Reset() {
    this.position.y = -Math.random() * ConfettiRibbon.bounds.y;
    this.position.x = Math.random() * ConfettiRibbon.bounds.x;
    this.prevPosition = new Vector2(this.position.x, this.position.y);
    this.velocityInherit = Math.random() * 2 + 4;
    this.time = Math.random() * 100;
    this.oscillationSpeed = Math.random() * 2.0 + 1.5;
    this.oscillationDistance = (Math.random() * 40 + 40);
    this.ySpeed = Math.random() * 40 + 80;
    const ci = Math.round(Math.random() * (colors.length - 1));
    this.frontColor = colors[ci][0];
    this.backColor = colors[ci][1];
    this.particles = new Array();
    for (let i = 0; i < this.particleCount; i++) {
      this.particles[i] = new EulerMass(this.position.x, this.position.y - i * this.particleDist, this.particleMass, this.particleDrag);
    }
  }

  Draw(g) {
    for (let i = 0; i < this.particleCount - 1; i++) {
      const p0 = new Vector2(this.particles[i].position.x + this.xOff, this.particles[i].position.y + this.yOff);
      const p1 = new Vector2(this.particles[i + 1].position.x + this.xOff, this.particles[i + 1].position.y + this.yOff);
      if (this.Side(this.particles[i].position.x, this.particles[i].position.y,
          this.particles[i + 1].position.x, this.particles[i + 1].position.y, p1.x, p1.y) < 0) {
        g.fillStyle = this.frontColor;
        g.strokeStyle = this.frontColor;
      } else {
        g.fillStyle = this.backColor;
        g.strokeStyle = this.backColor;
      }
      if (i === 0) {
        g.beginPath();
        g.moveTo(this.particles[i].position.x * window.devicePixelRatio, this.particles[i].position.y * window.devicePixelRatio);
        g.lineTo(this.particles[i + 1].position.x * window.devicePixelRatio, this.particles[i + 1].position.y * window.devicePixelRatio);
        g.lineTo(((this.particles[i + 1].position.x + p1.x) * 0.5) * window.devicePixelRatio,
          ((this.particles[i + 1].position.y + p1.y) * 0.5) * window.devicePixelRatio);
        g.closePath();
        g.stroke();
        g.fill();
        g.beginPath();
        g.moveTo(p1.x * window.devicePixelRatio, p1.y * window.devicePixelRatio);
        g.lineTo(p0.x * window.devicePixelRatio, p0.y * window.devicePixelRatio);
        g.lineTo(((this.particles[i + 1].position.x + p1.x) * 0.5) * window.devicePixelRatio,
            ((this.particles[i + 1].position.y + p1.y) * 0.5) * window.devicePixelRatio);
        g.closePath();
        g.stroke();
        g.fill();
      } else if (i === this.particleCount - 2) {
        g.beginPath();
        g.moveTo(this.particles[i].position.x * window.devicePixelRatio, this.particles[i].position.y * window.devicePixelRatio);
        g.lineTo(this.particles[i + 1].position.x * window.devicePixelRatio, this.particles[i + 1].position.y * window.devicePixelRatio);
        g.lineTo(((this.particles[i].position.x + p0.x) * 0.5) * window.devicePixelRatio,
          ((this.particles[i].position.y + p0.y) * 0.5) * window.devicePixelRatio);
        g.closePath();
        g.stroke();
        g.fill();
        g.beginPath();
        g.moveTo(p1.x * window.devicePixelRatio, p1.y * window.devicePixelRatio);
        g.lineTo(p0.x * window.devicePixelRatio, p0.y * window.devicePixelRatio);
        g.lineTo(((this.particles[i].position.x + p0.x) * 0.5) * window.devicePixelRatio,
          ((this.particles[i].position.y + p0.y) * 0.5) * window.devicePixelRatio);
        g.closePath();
        g.stroke();
        g.fill();
      } else {
        g.beginPath();
        g.moveTo(this.particles[i].position.x * window.devicePixelRatio, this.particles[i].position.y * window.devicePixelRatio);
        g.lineTo(this.particles[i + 1].position.x * window.devicePixelRatio, this.particles[i + 1].position.y * window.devicePixelRatio);
        g.lineTo(p1.x * window.devicePixelRatio, p1.y * window.devicePixelRatio);
        g.lineTo(p0.x * window.devicePixelRatio, p0.y * window.devicePixelRatio);
        g.closePath();
        g.stroke();
        g.fill();
      }
    }
  }

  Side(x1, y1, x2, y2, x3, y3) {
    return ((x1 - x2) * (y3 - y2) - (y1 - y2) * (x3 - x2));
  }

}
