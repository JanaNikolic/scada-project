import { MatDatepickerModule } from '@angular/material/datepicker';
import { AnalogInputsComponent } from './analog-inputs/analog-inputs.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import {MatTableModule} from '@angular/material/table';
import {MatButtonModule} from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import {AddUserComponent} from "./add-user/add-user.component";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import {AlarmComponent} from "./dialogs/alarm/alarm.component";
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatDialogModule } from '@angular/material/dialog';
import {MatNativeDateModule, MatOptionModule} from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { AnalogOutputsComponent } from './analog-outputs/analog-outputs.component';
import { OutputDialogComponent } from './dialogs/output-dialog/output-dialog.component';
import { InputDialogComponent } from './dialogs/input-dialog/input-dialog/input-dialog.component';
import { ValueDialogComponent } from './dialogs/value-dialog/value-dialog/value-dialog.component';
import { LoginComponent } from './login/login.component';
import { MaterialModule } from './material/material.module';
import { AlarmByPriorityComponent } from './reports/alarm-by-priority/alarm-by-priority.component';
import { MatMenuModule } from '@angular/material/menu';
import { MatSortModule } from '@angular/material/sort';
import { InputReportComponent } from './reports/input-report/input-report.component';
import { TagRecordsComponent } from './reports/tag-records/tag-records.component';
import { AlarmTimeRangeComponent } from './reports/alarm-time-range/alarm-time-range.component';
import { RecordsTimeRangeComponent } from './reports/records-time-range/records-time-range.component';
import {DatePipe} from "@angular/common";
import { TrendingComponent } from './trending/trending.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    AddUserComponent,
    FetchDataComponent,
    AlarmComponent,
    AnalogInputsComponent,
    AnalogOutputsComponent,
    OutputDialogComponent,
    InputDialogComponent,
    ValueDialogComponent,
    LoginComponent,
    AlarmByPriorityComponent,
    AlarmTimeRangeComponent,
    InputReportComponent,
    RecordsTimeRangeComponent,
    TagRecordsComponent,
    TrendingComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MatSnackBarModule,
    MatSlideToggleModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatPaginatorModule,
    MatCheckboxModule,
    MatTableModule,
    MatSortModule,
    MatDialogModule,
    MatMenuModule,
    MatDatepickerModule,
    MatOptionModule,
    MatButtonModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatNativeDateModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: 'home', component: HomeComponent,
      children: [
        { path: 'inputs', component: AnalogInputsComponent },
        { path: 'outputs', component: AnalogOutputsComponent },
      ],},
      { path: '', redirectTo: '/login', pathMatch: 'full' },
      { path: 'login', component: LoginComponent, pathMatch: 'full' },
      { path: 'add-user', component: AddUserComponent },
      { path: 'alarm', component: AlarmComponent },
      { path: 'alarm-by-priority', component: AlarmByPriorityComponent },
      { path: 'alarm-by-range', component: AlarmTimeRangeComponent },
      { path: 'current-values', component: InputReportComponent },
      { path: 'tag-records', component: TagRecordsComponent },
      { path: 'records-by-range', component: RecordsTimeRangeComponent },
      { path: 'trending', component: TrendingComponent },
    ]),
    BrowserAnimationsModule,
    MaterialModule
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
