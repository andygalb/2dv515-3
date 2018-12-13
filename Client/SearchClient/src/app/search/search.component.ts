import { Component, OnInit } from '@angular/core';
import {DataService, Result} from "../data.service";

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  results: Result[];
  query: string;
  searchMethod: string;


  displayedColumns: string[] = ['url', 'score','frequencyScore', 'locationScore','pageRankScore'];

  constructor(private dataService: DataService) { }

  ngOnInit() {
  }

  searchSwitcher(){
    console.log(this.searchMethod);
    switch(this.searchMethod){
      case "0":
        {
          console.log("In case 0");
        this.searchFrequency();
        break;
        }
      case "1":
      {
        this.searchFrequencyAndLocation();
        break;
      }
      case "2":
      {
        this.search();
        break;
      }


    }
  }
  searchFrequency(){
    this.dataService.getSearchFrequency(this.query)
      .subscribe((data: Result[]) =>
      {
        console.log(data);
        this.results = data;
      });
  }

  searchFrequencyAndLocation(){
    this.dataService.getSearchFrequencyLocation(this.query)
      .subscribe((data: Result[]) =>
      {
        console.log(data);
        this.results = data;
      });
  }

  search() {

    this.dataService.getSearch(this.query)
      .subscribe((data: Result[]) =>
      {
        console.log(data);
        this.results = data;
      });
  }

}
