export interface IDashboard {
    systems: any[];
}

export class Dashboard implements IDashboard {

    constructor(
        public systems : any[]
    ) { }

}