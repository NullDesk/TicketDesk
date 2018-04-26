import { AdContactService } from './../services/ad-contact.service';
import { UserDetails } from 'app/models/user-details';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-contact-info',
  templateUrl: './contact-info.component.html',
  styleUrls: ['./contact-info.component.css']
})
export class ContactInfoComponent implements OnInit {
  @Input()
  userName: string;
  user: UserDetails;

  constructor(private contactService: AdContactService) { }

  ngOnInit() {
    const { contactService, userName } = this;
    contactService.getContactCardInfo(userName)
      .subscribe(userDetails => {
        console.warn('userDetails', userDetails);
        this.user = userDetails;
      });
  }

}
