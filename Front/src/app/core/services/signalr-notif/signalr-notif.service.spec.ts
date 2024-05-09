import { TestBed } from '@angular/core/testing';

import { SignalrNotifService } from './signalr-notif.service';

describe('SignalrNotifService', () => {
  let service: SignalrNotifService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SignalrNotifService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
