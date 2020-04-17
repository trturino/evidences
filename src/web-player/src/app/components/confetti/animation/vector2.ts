export class Vector2 {

  static Lerp(vec0: Vector2, vec1: Vector2, t: number) {
    return new Vector2((vec1.x - vec0.x) * t + vec0.x, (vec1.y - vec0.y) * t + vec0.y);
  }

  static Scale(vec0, vec1) {
    return new Vector2(vec0.x * vec1.x, vec0.y * vec1.y);
  }

  static Min(vec0, vec1) {
    return new Vector2(Math.min(vec0.x, vec1.x), Math.min(vec0.y, vec1.y));
  }

  static Max(vec0, vec1) {
    return new Vector2(Math.max(vec0.x, vec1.x), Math.max(vec0.y, vec1.y));
  }

  static ClampMagnitude(vec0, len) {
    const vecNorm = vec0.Normalized;
    return new Vector2(vecNorm.x * len, vecNorm.y * len);
  }

  static Sub(vec0, vec1) {
    return new Vector2(vec0.x - vec1.x, vec0.y - vec1.y, vec0.z - vec1.z);
  }

  constructor(public x: number, public y: number, public z?: number) {
  }

  Length() {
    return Math.sqrt(this.SqrLength());
  }

  SqrLength() {
    return this.x * this.x + this.y * this.y;
  }

  Add(vec: Vector2) {
    this.x += vec.x;
    this.y += vec.y;
  }

  Sub(vec: Vector2) {
    this.x -= vec.x;
    this.y -= vec.y;
  }

  Div(f: number) {
    this.x /= f;
    this.y /= f;
  }

  Mul(f: number) {
    this.x *= f;
    this.y *= f;
  }

  Normalize() {
    const sqrLen = this.SqrLength();
    if (sqrLen !== 0) {
      const factor = 1.0 / Math.sqrt(sqrLen);
      this.x *= factor;
      this.y *= factor;
    }
  }
  Normalized() {
    const sqrLen = this.SqrLength();
    if (sqrLen !== 0) {
      const factor = 1.0 / Math.sqrt(sqrLen);
      return new Vector2(this.x * factor, this.y * factor);
    }
    return new Vector2(0, 0);
  }


}
