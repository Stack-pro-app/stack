package com.ProjectMana.ProjectManagementSpring.repo;

import com.ProjectMana.ProjectManagementSpring.enteties.project;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Component;

@Component
public interface projectRepo extends JpaRepository<project,Integer> {
    project findByprojectName (String name);
}
