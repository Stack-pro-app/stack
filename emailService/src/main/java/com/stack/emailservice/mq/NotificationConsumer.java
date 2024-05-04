package com.stack.emailservice.mq;

import com.stack.emailservice.request.EmailRequest;
import com.stack.emailservice.service.EmailSenderService;
import lombok.AllArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.amqp.rabbit.annotation.RabbitListener;
import org.springframework.stereotype.Component;

@Component
@AllArgsConstructor
@Slf4j
public class NotificationConsumer {

    private final EmailSenderService emailSenderService;

    @RabbitListener(queues = "name....")
    public void consumer(EmailRequest emailRequest) {
        log.info("Consumed {} from queue", emailRequest);
        emailSenderService.sendEmail(emailRequest.getToCustomerEmail(), emailRequest.getSubject(), emailRequest.getMessage());
    }
}
