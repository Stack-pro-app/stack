package com.ProjectMana.ProjectManagementSpring.repo;

import com.ProjectMana.ProjectManagementSpring.enteties.ScheduleEvent;
import com.ProjectMana.ProjectManagementSpring.enteties.project;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Component;

@Component
public interface scheduleRepo extends JpaRepository<ScheduleEvent,Integer> {
}
