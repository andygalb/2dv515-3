import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import {DataService} from './data.service';
import { HttpClientModule } from '@angular/common/http';

import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {
  MatToolbarModule, MatSelectModule, MatGridListModule, MatCardModule,
  MatExpansionModule, MatRadioModule, MatInputModule, MatTableModule
} from '@angular/material';
import {RouterModule, Routes} from "@angular/router";
import { SearchComponent } from './search/search.component';
import { NgxJsonViewerModule } from 'ngx-json-viewer';
import {FormsModule} from "@angular/forms";

const appRoutes: Routes = [
  { path: '',   redirectTo: '/search', pathMatch: 'full' },
  { path: 'search', component: SearchComponent },
];


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    SearchComponent,
  ],
  imports: [
    NgxJsonViewerModule,
    BrowserModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatExpansionModule,
    MatTableModule,
    MatToolbarModule,
    MatInputModule,
    MatRadioModule,
    MatSelectModule,
    MatGridListModule,
    MatCardModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [DataService],
  bootstrap: [AppComponent]
})
export class AppModule { }
