pipeline {
    agent any
    environment {
        REPO_FE = "bookings/frontend"
        REPO_API = "bookings/api"
        PRIVATE_REPO_FE = "${PRIVATE_DOCKER_REGISTRY}/${REPO_FE}"
        PRIVATE_REPO_API = "${PRIVATE_DOCKER_REGISTRY}/${REPO_API}"
        TAG = "${BUILD_TIMESTAMP}"
    }
    stages {
        stage('Git clone') {
            steps {
                git branch: 'main',
                    url: 'https://github.com/LivingSkySchoolDivision/Bookings.git'
            }
        }
        stage('Docker build - API') {
            steps {
                dir("src") {
                    sh "docker build -f Dockerfile-API -t ${PRIVATE_REPO_API}:latest -t ${PRIVATE_REPO_API}:${TAG} ."
                }
            }
        }
        stage('Docker build - Frontend') {
            steps {
                dir("src") {
                    sh "docker build -f Dockerfile-WebFrontEnd -t ${PRIVATE_REPO_FE}:latest -t ${PRIVATE_REPO_FE}:${TAG} ."
                }
            }
        }
        stage('Docker push') {
            steps {
                sh "docker push ${PRIVATE_REPO_FE}:${TAG}"
                sh "docker push ${PRIVATE_REPO_FE}:latest"
                sh "docker push ${PRIVATE_REPO_API}:${TAG}"
                sh "docker push ${PRIVATE_REPO_API}:latest"
            }
        }
    }
    post {
        always {
            deleteDir()
        }
    }
}