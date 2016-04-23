import { Component } from 'angular2/core';

import { DashboardComponent } from './dashboard/dashboard.component';

@Component({
    selector: 'cs-app',
    template: '<div><cs-dashboard>Loading...</cs-dashboard></div>',
    directives: [DashboardComponent]
})
export class AppComponent {
}