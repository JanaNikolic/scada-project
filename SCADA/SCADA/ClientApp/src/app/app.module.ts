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
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatDialogModule } from '@angular/material/dialog';
import { MatOptionModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { AnalogOutputsComponent } from './analog-outputs/analog-outputs.component';
import { OutputDialogComponent } from './dialogs/output-dialog/output-dialog.component';
import { InputDialogComponent } from './dialogs/input-dialog/input-dialog/input-dialog.component';
import { ValueDialogComponent } from './dialogs/value-dialog/value-dialog/value-dialog.component';
import { LoginComponent } from './login/login.component';
import { MaterialModule } from './material/material.module';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    AddUserComponent,
    FetchDataComponent,
    AnalogInputsComponent,
    AnalogOutputsComponent,
    OutputDialogComponent,
    InputDialogComponent,
    ValueDialogComponent,
    LoginComponent
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
    MatDialogModule,
    MatOptionModule,
    MatButtonModule,
    RouterModule.forRoot([
      { path: 'home', component: HomeComponent,
      children: [
        { path: 'inputs', component: AnalogInputsComponent },
        { path: 'outputs', component: AnalogOutputsComponent },
      ],},
      { path: '', redirectTo: '/login', pathMatch: 'full' },
      { path: 'login', component: LoginComponent, pathMatch: 'full' },
      { path: 'add-user', component: AddUserComponent },
      { path: 'trending', component: FetchDataComponent },
    ]),
    BrowserAnimationsModule,
    MaterialModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
