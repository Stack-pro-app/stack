package com.ProjectMana.ProjectManagementSpring.enteties;

import com.fasterxml.jackson.annotation.JsonProperty;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.Id;
import jakarta.persistence.TemporalType;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.springframework.data.jpa.repository.Temporal;

import java.util.Date;

@Entity
@Data
@AllArgsConstructor
@NoArgsConstructor
public class ScheduleEvent {
  @Id
@GeneratedValue
  private Integer id;

  @JsonProperty("StartTime")

  private Date startTime;

  @JsonProperty("EndTime")

  private Date endTime;

  @JsonProperty("Subject")
  private String subject;

  @JsonProperty("IsAllDay")
  private boolean isAllDay;

  @JsonProperty("StartTimezone")
  private String startTimezone;

  @JsonProperty("EndTimezone")
  private String endTimezone;

  @JsonProperty("RecurrenceRule")
  private String recurrenceRule;

  @JsonProperty("RecurrenceID")
  private String recurrenceID;

  @JsonProperty("RecurrenceException")
  private String recurrenceException;

  @JsonProperty("Location")
  private String location;

  @JsonProperty("Description")
  private String description;
}
