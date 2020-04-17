import { Vector2 } from './vector2';

export class EulerMass {

  public position: Vector2;
  public force: Vector2;
  public velocity: Vector2;

  constructor(private x: number, private y: number, private mass: number, private drag: number) {
    this.position = new Vector2(x, y);
    this.force = new Vector2(0, 0);
    this.velocity = new Vector2(0, 0);
  }

  AddForce(f: Vector2) {
    this.force.Add(f);
  }

  Integrate(dt: number) {
    const acc = this.CurrentForce(this.position);
    acc.Div(this.mass);
    const posDelta = new Vector2(this.velocity.x, this.velocity.y);
    posDelta.Mul(dt);
    this.position.Add(posDelta);
    acc.Mul(dt);
    this.velocity.Add(acc);
    this.force = new Vector2(0, 0);
  }

  CurrentForce(pos, vel?) {
    const totalForce = new Vector2(this.force.x, this.force.y);
    const speed = this.velocity.Length();
    const dragVel = new Vector2(this.velocity.x, this.velocity.y);
    dragVel.Mul(this.drag * this.mass * speed);
    totalForce.Sub(dragVel);
    return totalForce;
  }
}
