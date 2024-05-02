package com.stack.emailservice.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.mail.SimpleMailMessage;
import org.springframework.mail.javamail.JavaMailSender;
import org.springframework.stereotype.Service;

@Service
public class EmailSenderService {
    @Autowired
    private JavaMailSender mailSender;
    public void sendEmail(String toEmail,String subject,String body){
        SimpleMailMessage message=new SimpleMailMessage();
        message.setSubject(subject);
        message.setTo(toEmail);
        message.setText(body);
        message.setFrom("oussama.zaoui@etu.uae.ac.ma");
        this.mailSender.send(message);
        System.out.println("mail sent successfully .......");
    }
}
