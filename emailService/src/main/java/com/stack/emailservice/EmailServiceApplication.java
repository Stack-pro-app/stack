package com.stack.emailservice;

import com.stack.emailservice.service.EmailSenderService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.context.event.ApplicationReadyEvent;
import org.springframework.context.event.EventListener;

@SpringBootApplication
public class EmailServiceApplication {

	@Autowired
	private  EmailSenderService emailSenderService;
	public static void main(String[] args) {

		SpringApplication.run(EmailServiceApplication.class, args);

	}
	@EventListener(ApplicationReadyEvent.class)
	public void sendMail(){
		emailSenderService.sendEmail("zaouio534@gmail.com","test","first email");
	}


}
