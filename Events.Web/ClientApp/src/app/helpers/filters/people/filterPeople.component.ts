import { AfterViewInit, Component, Input, OnChanges, OnInit} from "@angular/core";
import { UntypedFormControl } from "@angular/forms";
import { map, startWith, Observable, first, Subscription } from "rxjs";
import { displayPeople } from "../../../model/displayPeople.model";
import { Person } from "../../../model/person.model";
import { AppService } from "../../../server/app.service";

@Component({
  selector: 'app-filter-People',
  templateUrl: './filterPeople.component.html'
})
export class FilterPeopleComponent implements AfterViewInit {

  @Input() dataPeople: displayPeople;

  optionsPersons: Person[];
  personsOptions: Observable<Person[]>;
  personControl = new UntypedFormControl('');


  constructor(private service: AppService) {
  }

 
  ngAfterViewInit(): void {
    this.GetAllPerson();
  }


  GetAllPerson() {
    this.service.getPeople('FirstName', 'desc', 0, 1000, '')
      .pipe(first())
      .subscribe(
        data => {
        //  console.log('Current data: ', data);

          this.optionsPersons = data==null?[]:data ;

          this.personsOptions = this.personControl.valueChanges.pipe(
            startWith(''),
            map(value => {
              const name = typeof value === 'string' ? value : value?.name;
              return name=='' ? this.filterPeople(name as string) : this.optionsPersons.slice();
            }),
          );
        },
        error => console.log('Error Getting Position: ', error)

      );
  }

  displayPeople = (user: Person): string => {

    if (this.dataPeople != undefined) {
      if (user.id != undefined) {

        this.dataPeople.personSelected = user;
        this.service.sendUpdatePeople(this.dataPeople);
      } else {
        if (this.dataPeople.personSelected != undefined) {
          this.personControl.setValue(this.dataPeople.personSelected);

        }
        else {
          console.log("No users found!");
        }
      }

    }
    return user && user.firstName ? user.firstName : '';
  }

  private filterPeople(name: string): Person[] {

    const filterValue = name.toLowerCase();

    return this.optionsPersons.filter(option => option.firstName.toLowerCase().includes(filterValue)
      || option.firstSurname?.toLowerCase().includes(filterValue)
      || option.secondName?.toLowerCase().includes(filterValue)
      || option.secondSurname?.toLowerCase().includes(filterValue)
      || option.dateOfBirth?.toString().toLowerCase().includes(filterValue)
      || option.dateOfDeath?.toString().toLowerCase().includes(filterValue));
  }

}
