package com.stack.emailservice.request;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;

@Data
@Builder
@AllArgsConstructor

public class EmailRequest {
        String subject;
        String toCustomerEmail;
        String message;
}

