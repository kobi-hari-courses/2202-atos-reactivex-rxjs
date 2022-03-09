import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { DataService } from 'src/app/services/data.service';

@Component({
  selector: 'app-data-presenter',
  templateUrl: './data-presenter.component.html',
  styleUrls: ['./data-presenter.component.css']
})
export class DataPresenterComponent implements OnInit, OnDestroy {

  values: string[] = [];
  subscription!: Subscription;


  constructor(private dataService: DataService) { }

  ngOnInit(): void {
    this.subscription = this.dataService.getData().subscribe({
      next: val => this.values.push(`next: ${val}`), 
      complete: () => this.values.push(`complete`), 
      error: err => this.values.push(`err: ${err}`), 
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

}
