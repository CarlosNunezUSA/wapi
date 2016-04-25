import { Component } from 'angular2/core';

import { DashboardComponent } from './dashboard/dashboard.component';

@Component({
    selector: 'cc-app',
    template: '<div><cc-dashboard>Loading...</cc-dashboard></div>',
    directives: [DashboardComponent]
})
export class AppComponent {
}