export class MatchModel {
    constructor(init? : Partial<MatchModel>) {
        Object.assign(this, init);
    }

    time: string;
    court: string;
    teamA: string;
    teamAHref: string;
    teamB: string;
    teamBHref: string;
    score: string;
    scoreHref: string;
}