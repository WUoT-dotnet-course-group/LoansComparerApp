export class User {
  constructor(
    public email: string,
    private _isBankEmployee: boolean,
    private _token: string
  ) {}

  get isBankEmployee() {
    return this._isBankEmployee;
  }

  get token() {
    return this._token;
  }
}
