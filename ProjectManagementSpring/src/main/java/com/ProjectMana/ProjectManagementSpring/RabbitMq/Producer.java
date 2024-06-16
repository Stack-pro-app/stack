package com.ProjectMana.ProjectManagementSpring.RabbitMq;

import com.ProjectMana.ProjectManagementSpring.DTO.userDTO;
import com.ProjectMana.ProjectManagementSpring.enteties.UserT;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.amqp.rabbit.core.RabbitTemplate;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

@Service
public class Producer {
    @Value("${rabbitmq.exchange.name}")
    private String exchangeName;

    @Value("${rabbitmq.key.name}")
    private String keyName;

    @Value("${rabbitmq.queue.name}")
    private String queueName;

    private static final Logger LOGGER = LoggerFactory.getLogger(Producer.class);
    private RabbitTemplate rabbitTemplate ;

    public Producer(RabbitTemplate rabbitTemplate) {
        this.rabbitTemplate = rabbitTemplate;
    }
    public void sendJson(userDTO user){

        //LOGGER.info("producing: {}", user);
       // userDTO ii = new userDTO(154,"sadf","mm","rjd@vv.com");
        rabbitTemplate.convertAndSend("",this.queueName,user);
        //rabbitTemplate.convertAndSend(this.exchangeName,this.keyName,user);

    }
}
