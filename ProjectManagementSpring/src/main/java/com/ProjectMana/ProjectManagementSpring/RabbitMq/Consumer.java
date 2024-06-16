package com.ProjectMana.ProjectManagementSpring.RabbitMq;

import com.ProjectMana.ProjectManagementSpring.DTO.userDTO;
import com.ProjectMana.ProjectManagementSpring.enteties.UserT;
import com.ProjectMana.ProjectManagementSpring.services.userService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.amqp.rabbit.annotation.RabbitListener;
import org.springframework.stereotype.Service;

@Service
public class Consumer {
    private userService userService;

    public Consumer(com.ProjectMana.ProjectManagementSpring.services.userService userService) {
        this.userService = userService;
    }

    private static final Logger LOGGER = LoggerFactory.getLogger(Consumer.class);
    @RabbitListener(queues = "${rabbitmq.queue.name}")
    public void consume(userDTO user) {
        LOGGER.info("recieving : {}", user);
        this.userService.post(user) ;

    }
}
