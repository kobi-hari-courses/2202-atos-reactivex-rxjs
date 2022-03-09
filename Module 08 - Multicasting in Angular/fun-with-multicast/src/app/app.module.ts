import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { DataPresenterComponent } from './components/data-presenter/data-presenter.component';

@NgModule({
  declarations: [
    AppComponent,
    DataPresenterComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
