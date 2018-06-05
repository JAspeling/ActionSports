import { MatchModel } from "./MatchModel";

export class StandingModel {
    constructor(init? : Partial<StandingModel>) {
        Object.assign(this, init);
    }

    isExpanded: boolean = false;
    date: string;
    matches: MatchModel[] = [];
}