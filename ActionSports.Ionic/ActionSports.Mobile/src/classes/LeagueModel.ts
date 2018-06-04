export class LeagueModel {
    constructor(init?: Partial<LeagueModel>) {
        Object.assign(this, init);
    }

    title: string;
    fixture: string;
    standing: string;
}
